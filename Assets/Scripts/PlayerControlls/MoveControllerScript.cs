using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveController : MonoBehaviour
{
    
    public float JumpForce = 2f;
    public float Force = 10;
    public float AdditionalGravity = 2;
    public GameObject Explosion;

    public AudioClip ExplosionClip;
    public AudioClip JumpClip1;
    public AudioClip JumpClip2;

    protected Rigidbody _rigidbody;
    protected InputAction _moveActiondy;
    protected InputAction _jumpAction;
    protected Vector3 _move;
    protected Vector3 _spawnPoint;
    protected AudioSource _audioSource;

    private bool _stopMoving = false;
    private Collider _collider;
    private bool _jumpSound = false;

    protected virtual void Start()
    {
        // actions
        _moveActiondy = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        // variable assigment
        _rigidbody = GetComponent<Rigidbody>();

        // events
        WallsScript.e_WallCollision += WallCollision;
        DialogueHandlerScrpit.e_SetSpawn += SetSpawnPoint;
        _spawnPoint = new Vector3(0, 0.5f, 0);

        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            _collider = gameObject.GetComponent<BoxCollider>();
        }
        else
        {
            _collider = gameObject.GetComponent<SphereCollider>();
        }

        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnDestroy()
    {
        // this fixes error on scene change
        WallsScript.e_WallCollision -= WallCollision;
        DialogueHandlerScrpit.e_SetSpawn -= SetSpawnPoint;
    }

    protected virtual void FixedUpdate()
    {
        if (_stopMoving)
        {
            return;
        }
        _rigidbody.AddForce(0, -AdditionalGravity, 0, ForceMode.Force);

        if (_moveActiondy.IsPressed())
        {
            _move = _moveActiondy.ReadValue<Vector2>();
            _rigidbody.AddForce(_move.x * Force * Time.deltaTime, 0, _move.y * Force * Time.deltaTime, ForceMode.Impulse);
        }
        if (_jumpAction.IsPressed())
        {
            Jump();
        }
    }

    protected virtual void Jump()
    {
        if (TouchesGround())
        {
            _audioSource.clip = _jumpSound ? JumpClip1: JumpClip2; // just so it isn't repetitive
            _jumpSound = !_jumpSound;
            _audioSource.Play();
            _rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }
    }

    public virtual void WallCollision(object obj, EventArgs e)
    {
        // make player invisible and untargetable
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _collider.enabled = false;

        // make player stop moving on all vectors
        _stopMoving = true;
        _rigidbody.isKinematic = true;

        // make explosion in place of player
        Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0, 0));

        _audioSource.clip = ExplosionClip;
        _audioSource.Play();

        // start delayed spawn
        StartCoroutine(DelaySpawn());
    }

    public virtual void SetSpawnPoint(object obj, Vector3 spawnPoint)
    {
        _spawnPoint  = spawnPoint;
    }

    public virtual bool TouchesGround()
    {
        // checking if there is ground underneath using raycast,
        // better than managing it from perspective of ground
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(2f);
        
        gameObject.transform.SetPositionAndRotation(_spawnPoint, Quaternion.Euler(-90, 0, 0)); 
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        _collider.enabled = true;
        _stopMoving = false;
        _rigidbody.isKinematic = false;
    }
}
