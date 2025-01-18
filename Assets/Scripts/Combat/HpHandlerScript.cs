using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HpHandlerScript : MonoBehaviour
{
    public static event EventHandler e_HpLost;

    public float Hp = 10;

    private float _initialHp;

    protected virtual void Start()
    {
        _initialHp = Hp;
    }

    protected virtual void Update()
    {
        if (Hp <= 0)
        {
            Hp = _initialHp;
            e_HpLost?.Invoke(this, EventArgs.Empty);
        }
    }

    protected virtual void RemoveHp(float dmg)
    {
        Hp -= dmg;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<BulletScript>().TargetTag == gameObject.tag)
        {
            RemoveHp(other.gameObject.GetComponent<BulletScript>().Damage);
            Destroy(other.gameObject);
        }
    }
}