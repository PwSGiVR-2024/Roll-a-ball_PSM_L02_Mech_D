using UnityEngine;

public class EnemyHpHandlerScript : HpHandlerScript
{
    public GameObject Explosion;

    protected override void Update()
    {
        if (Hp <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
