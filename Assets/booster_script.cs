using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booster_script : MonoBehaviour
{
    public float boost_force = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<MoveController>().m_Rigidbody.AddForce(0, boost_force, 0, ForceMode.Impulse);
        print("skacz");
    }
}
