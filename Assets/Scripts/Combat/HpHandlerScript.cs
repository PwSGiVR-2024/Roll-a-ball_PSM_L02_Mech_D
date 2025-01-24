using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HpHandlerScript : MonoBehaviour
{

    public static event EventHandler e_HpLost;

    // this field is shared with enemy script, so the boss and the major emenies
    // could have thier Hp bars if needed
    public Slider HpBar;

    public float Hp = 10;

    private float _initialHp;

    protected virtual void Start()
    {
        _initialHp = Hp;
    }

    protected virtual void Update()
    {
        HpBar.value = Hp;
        if (Hp <= 0)
        {
            Hp = _initialHp;
            e_HpLost?.Invoke(this, EventArgs.Empty);
            HpBar.value = Hp;
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