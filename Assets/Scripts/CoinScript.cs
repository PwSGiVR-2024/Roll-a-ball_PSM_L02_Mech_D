using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

public class CoinScript : MonoBehaviour
{
    Rigidbody _Rigidbody;
    public UnityEvent onCoinCollection;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Rotate(20 * Time.deltaTime,0,0);
    }

    void OnTriggerEnter(Collider collider)
    {
        onCoinCollection.Invoke();
        gameObject.SetActive(false);
    }
}
