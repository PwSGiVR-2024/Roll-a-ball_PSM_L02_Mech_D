using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHpHandlerScript : HpHandlerScript
{
    public GameObject Explosion;
    public AudioClip ExplosionSound;

    private AudioSource _audioSource;
    private bool _doOnce = true;

    protected override void Start()
    {
        base.Start();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        if (HpBar != null) // also has to be set to active in the enemy script (ex. BossScript)
        {
            HpBar.value = Hp;
        }
        if (Hp <= 0 && _doOnce)
        {
            _audioSource.clip = ExplosionSound;
            print(_audioSource.clip != null);
            _audioSource.Play();
            Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0, 0));
            if (HpBar != null)
            {
                HpBar.gameObject.SetActive(false);
            }

            StartCoroutine(DestroyTurret());
            DisableTurret();
             _doOnce = false;
        }
    }

    private void DisableTurret()
    {
        if (GetComponent<TurretScript>() != null)
        {
            var turretScript = GetComponent<TurretScript>();
            turretScript.enabled = false;
        }

        if (GetComponent<CapsuleCollider>() != null)
        {
            GetComponent<CapsuleCollider>().enabled = false;
        }

        if (GetComponent<BoxCollider>() != null)
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        GetComponent<MeshRenderer>().enabled = false;
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        if (meshRenderers != null)
        {
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }
    }

    IEnumerator DestroyTurret()
    {
        yield return new WaitForSeconds(ExplosionSound.length);
        Destroy(gameObject);
    }
}
