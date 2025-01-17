using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    public GameObject Ball;
    public MoveController BallController;
    private Vector3 _CameraPositon;

    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.Find("Player");
        BallController = GetComponent<MoveController>();
        _CameraPositon = transform.position - Ball.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = _CameraPositon + Ball.transform.position;
    }
}
