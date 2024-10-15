using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collider)
    {
        collider.gameObject.GetComponent<MoveController>().touchesGround = true;
    }

    private void OnCollisionExit(Collision collider)
    {
        collider.gameObject.GetComponent<MoveController>().touchesGround = false;
    }
}