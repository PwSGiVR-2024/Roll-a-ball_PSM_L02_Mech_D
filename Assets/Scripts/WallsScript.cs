using System;
using UnityEngine;

public class WallsScript : MonoBehaviour
{

    public static event EventHandler<GameObject> e_WallCollision;

    // for walls
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            e_WallCollision?.Invoke(this, gameObject);
        }
    }

    // for asteroids
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            e_WallCollision?.Invoke(this, gameObject);
        }
    }
}
