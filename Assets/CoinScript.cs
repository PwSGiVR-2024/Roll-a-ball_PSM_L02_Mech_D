using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime,0,0);
        
    }

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.GetComponent<MoveController>().score = +1;
        print("Zdoby�e� punkt !");
        //Object.Destroy(gameObject);
        gameObject.GetComponentInParent<CoinCounter>().count();
        gameObject.SetActive(false);
    }
}