using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public int PathToTravel = 10;
    public float speed = 3f;
    private float _initialHight;
    private bool _goUp;
    private float _targetHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _initialHight = transform.position.y;
        _targetHeight = _initialHight + PathToTravel;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_goUp || transform.position.y >= _targetHeight)
        {
            return;
        }
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _goUp = true;
        }
    }
}
