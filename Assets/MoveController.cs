using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public bool touchesGround;
    float additionalGravity = 2;

    public int score = 0;

    InputAction moveAction;
    public float force = 10;
    private Vector3 move;

    InputAction jumpAction;
    float jumpForce = 3F;


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_Rigidbody.AddForce(0, -additionalGravity, 0, ForceMode.Force);

        if (moveAction.IsPressed())
        {
            move = moveAction.ReadValue<Vector2>();
            m_Rigidbody.AddForce(move.x*force*Time.deltaTime, 0, move.y * force * Time.deltaTime, ForceMode.Impulse);
        }
        if (jumpAction.IsPressed())
        {
            jump();
        }

    }

    void jump()
    {
        if (touchesGround)
        {
            m_Rigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    public void wallColision()
    {
        print("You touched a wall looser!");
        Vector3 resetLocation = Vector3.zero;
        resetLocation[1] = 0.5F;
        gameObject.transform.SetPositionAndRotation(resetLocation, Quaternion.Euler(0, 0, 0));
    }
}
