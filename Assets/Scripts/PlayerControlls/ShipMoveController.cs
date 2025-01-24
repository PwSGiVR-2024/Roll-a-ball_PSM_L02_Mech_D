using UnityEngine;

public class ShipMoveController : MoveController
{

    public GameObject BulletPrefab;
    public float FireRate = 0.5f;

    private float _lastFired;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        _lastFired = Time.time;
        HpHandlerScript.e_HpLost += WallCollision;
    }

    protected override void Jump()
    {
        if(Time.time > _lastFired + FireRate)
        {
        _audioSource.clip = JumpClip1;
        _audioSource.Play();

        GameObject bullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y -2, transform.position.z), Quaternion.Euler(new Vector3(0,90,90)));
        bullet.GetComponent<BulletScript>().OwnerTag = this.gameObject.tag;
        _lastFired = Time.time;
        }
    }
}
