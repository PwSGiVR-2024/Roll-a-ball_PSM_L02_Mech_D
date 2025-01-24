using System;
using System.Threading;
using UnityEngine;

public class BossScript : TurretScript
{
    public static event EventHandler<int> e_BossKilled;

    public GameObject BossBullet;
    public Vector3 rotation;
    public int AngledBulletsCount = 11;
    public int AngledBulletsStep = 5;
    public int BossKillTaskId = 7;

    private bool _mix = false;

    protected void FirePattern()
    {
        int beggining = - 90 + (AngledBulletsCount / 2) * AngledBulletsStep;
        if (_mix)
        {
            beggining += AngledBulletsStep/2;
            _mix = false;
            base.Fire(); // every 2 shots shoot singular ones that are red i direction of player
        }
        else
        {
            _mix = true;
        }
        for (int i = 0;  i <= AngledBulletsCount; i++)
        {
            GameObject bullet = Instantiate(BossBullet, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(new Vector3(0, beggining - (AngledBulletsStep *  i), 90)));
            bullet.GetComponent<BulletScript>().OwnerTag = this.gameObject.tag;
        }
    }

    protected override void Update()
    {
        if (!_playerDetected)
        {
            return;
        }
        if (Time.time > _lastFired + FireRate) // fast purple pattern
        {
            FirePattern();
            _lastFired = Time.time + UnityEngine.Random.Range(0f, RandomFireDelay);
        }
    }

    private void OnDestroy() // runs when destroyed
    {
        e_BossKilled?.Invoke(this, BossKillTaskId);
    }
}
