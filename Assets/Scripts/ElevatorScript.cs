using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public int PathToTravel = 10;
    public float speed = 3f;
    private float _InitialHight;
    private bool _GoUp;
    private float _TargetHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _InitialHight = transform.position.y;
        _TargetHeight = _InitialHight + PathToTravel;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_GoUp || transform.position.y >= _TargetHeight)
        {
            return;
        }
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _GoUp = true;
        }
    }
}
