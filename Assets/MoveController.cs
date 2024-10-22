using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public int score = 0;
    public bool touchesGround;
    //float forceX = 0, forceZ = 0;
    float force = 0.1F;
    float additionalGravity = 2;
    float jumpForce = 3F;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Rigidbody.AddForce(0, -additionalGravity, 0, ForceMode.Force);
        if (Input.GetKey(KeyCode.W))
        {
            //forceZ = 1;
            m_Rigidbody.AddForce(0, 0, force, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //forceZ = -1;
            m_Rigidbody.AddForce(0, 0, -force, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //forceX= 1;
            m_Rigidbody.AddForce(force, 0, 0, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //forceX = -1;
            m_Rigidbody.AddForce(-force, 0, 0, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.Space))
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
