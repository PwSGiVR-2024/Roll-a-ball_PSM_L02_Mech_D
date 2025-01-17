using UnityEngine;

public class EnemyHpHandlerScript : HpHandlerScript
{

    protected override void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
