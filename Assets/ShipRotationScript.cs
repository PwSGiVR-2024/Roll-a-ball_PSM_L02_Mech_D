using UnityEngine;

public class ShipRotationScript : MonoBehaviour
{
    public Rigidbody _rb;
    private Quaternion _baseRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _baseRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        print(_rb.linearVelocity);
        if(_rb.linearVelocity.x > 1)
        {   
            

            transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
        }
    }
}
