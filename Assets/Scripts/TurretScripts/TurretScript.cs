using System.Threading;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float FireRate = 1f;
    public float Damage = 1;
    public Transform Player;
    public float RandomFireDelay = 0.5f;

    private bool _playerDetected = false;
    private float _lastFired;
    private Quaternion _desiredRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Player == null)
        {
            print("Player is null, the turret will not work properly");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerDetected)
        {
            return;
        }

        if (Time.time > _lastFired + FireRate)
        {
            Fire();
            // makes it so the turrets don't all fire at the same time
            _lastFired = Time.time + Random.Range(0f, RandomFireDelay);
        }
    }

    private void Fire()
    {
        CalculateDesiredRotation();

        GameObject bullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, 0, transform.position.z), _desiredRotation);
        bullet.GetComponent<BulletScript>().OwnerTag = this.gameObject.tag;
        print("Turret firing");
    }

    void CalculateDesiredRotation()
    {
        Vector3 targetDirection = Player.position - transform.position; // Direct vector to the player

        // Calculate the rotation needed to face the target direction, but only around the Y-axis
        _desiredRotation = Quaternion.LookRotation(targetDirection);
        _desiredRotation.x = 0;
        _desiredRotation.z = 0;
        _desiredRotation *= Quaternion.Euler(new Vector3(0, 90, 90)); // this is needed becouse the bullet is rotated itself, so that the rotations match
    }

    public void OnPlayerDetect()
    {
        _playerDetected = true;
    }

    public void OnPlayerLeave()
    {
        _playerDetected = false;
    }
}
