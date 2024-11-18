using System;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    private Vector3 _SpawnPosition;
    public static event EventHandler<Vector3> e_SetSpawn;
    void Start()
    {
        _SpawnPosition = transform.position;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            e_SetSpawn?.Invoke(this, _SpawnPosition);
        }
    }
}
