using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booster_script : MonoBehaviour
{
    public float boost_force = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(0, boost_force, 0, ForceMode.Impulse);
    }
}
