using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{

    public GameObject ball;
    public MoveController ballController;
    private Vector3 camera_positon;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Player");
        ballController = GetComponent<MoveController>();
        camera_positon = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = camera_positon + ball.transform.position;
    }
}
