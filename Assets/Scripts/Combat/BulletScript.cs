using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string TargetTag = "Enemy";
    public string OwnerTag = "Player";
    public float Speed = 1.0f;
    public float TimeToLive = 3f;
    public float Damage = 1;

    private float _CreationTime;

    private void Start()
    {
        _CreationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _CreationTime + TimeToLive)
        {
            Destroy(gameObject, gameObject.GetComponent<TrailRenderer>().time);
            return;
        }
        transform.Translate(transform.right * Time.deltaTime * Speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "DontDestroyBullets" || other.gameObject.tag == "Bullet")
        {
            return;
        }
        if (other.gameObject.tag != OwnerTag && other.gameObject.tag != TargetTag)
        {
            Destroy(gameObject);
            return;
        }
    }
}
