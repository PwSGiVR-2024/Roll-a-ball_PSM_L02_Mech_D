using Unity.VisualScripting;
using UnityEngine;

public class EnemyHpHandlerScript : HpHandlerScript
{
    public GameObject Explosion;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (HpBar != null) // also has to be set to active in the enemy script (ex. BossScript)
        {
            HpBar.value = Hp;
        }
        if (Hp <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0, 0));
            if (HpBar != null)
            {
                HpBar.gameObject.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}
