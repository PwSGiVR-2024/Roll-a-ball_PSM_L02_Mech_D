using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float JumpForce = 2f;
    public float Force = 10;

    float AdditionalGravity = 2;
    InputAction MoveAction;
    InputAction JumpAction;

    private Vector3 _Move;

    void Start()
    {
        // actions
        MoveAction = InputSystem.actions.FindAction("Move");
        JumpAction = InputSystem.actions.FindAction("Jump");

        // variable assigment
        Rigidbody = GetComponent<Rigidbody>();

        // events
        WallsScript.e_WallCollision += WallCollision;
    }

    private void OnDestroy()
    {
        // this fixes error on scene change
        WallsScript.e_WallCollision -= WallCollision;
    }

    void FixedUpdate()
    {
        Rigidbody.AddForce(0, -AdditionalGravity, 0, ForceMode.Force);

        if (MoveAction.IsPressed())
        {
            _Move = MoveAction.ReadValue<Vector2>();
            Rigidbody.AddForce(_Move.x * Force * Time.deltaTime, 0, _Move.y * Force * Time.deltaTime, ForceMode.Impulse);
        }
        if (JumpAction.IsPressed())
        {
            Jump();
        }
    }

    void Jump()
    {
        if (TouchesGround())
        {
            Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }
    }

    public void WallCollision(object obj, EventArgs e)
    {
        gameObject.transform.SetPositionAndRotation(new Vector3(0,0.5f,0), Quaternion.Euler(0, 0, 0));
    }

    public bool TouchesGround()
    {
        // checking if there is ground underneath using raycast,
        // better than managing it from perspective of ground
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    
}
