using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    private GameObject _ball;
    public MoveController BallController;
    private Vector3 _CameraPositon;

    // Start is called before the first frame update
    void Start()
    {
        _ball = GameObject.Find("Player");
        BallController = GetComponent<MoveController>();
        _CameraPositon = transform.position - _ball.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = _CameraPositon + _ball.transform.position;
    }
}
