using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static WallsScript;

public class MoveController : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    float additionalGravity = 2;

    InputAction moveAction;
    public float force = 10;
    private Vector3 move;

    InputAction jumpAction;
    public float jumpForce = 2f;


    void Start()
    {
        // actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        // variable assigment
        m_Rigidbody = GetComponent<Rigidbody>();

        // events
        e_WallCollision += wallColision;
    }

    private void OnDestroy()
    {
        // this fixes error on scene change
        e_WallCollision -= wallColision;
    }

    void FixedUpdate()
    {
        m_Rigidbody.AddForce(0, -additionalGravity, 0, ForceMode.Force);

        if (moveAction.IsPressed())
        {
            move = moveAction.ReadValue<Vector2>();
            m_Rigidbody.AddForce(move.x * force * Time.deltaTime, 0, move.y * force * Time.deltaTime, ForceMode.Impulse);
        }
        if (jumpAction.IsPressed())
        {
            jump();
        }
    }

    void jump()
    {
        if (touches_ground())
        {
            m_Rigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    public void wallColision(object obj, EventArgs e)
    {
        gameObject.transform.SetPositionAndRotation(new Vector3(0,0.5f,0), Quaternion.Euler(0, 0, 0));
    }

    public bool touches_ground()
    {
        // checking if there is ground underneath using raycast,
        // better than managing it from perspective of ground
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    
}
