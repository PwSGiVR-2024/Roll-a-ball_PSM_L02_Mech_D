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

    protected Rigidbody _Rigidbody;
    protected InputAction _MoveAction;
    protected InputAction _JumpAction;
    protected Vector3 _Move;
    protected Vector3 _SpawnPoint;

    private bool _stopMoving = false;

    protected virtual void Start()
    {
        // actions
        _MoveAction = InputSystem.actions.FindAction("Move");
        _JumpAction = InputSystem.actions.FindAction("Jump");

        // variable assigment
        _Rigidbody = GetComponent<Rigidbody>();

        // events
        WallsScript.e_WallCollision += WallCollision;
        SpawnPointScript.e_SetSpawn += SetSpawnPoint;
        _SpawnPoint = new Vector3(0, 0.5f, 0);
    }

    protected virtual void OnDestroy()
    {
        // this fixes error on scene change
        WallsScript.e_WallCollision -= WallCollision;
        SpawnPointScript.e_SetSpawn -= SetSpawnPoint;
    }

    protected virtual void FixedUpdate()
    {
        if (_stopMoving)
        {
            return;
        }
        _Rigidbody.AddForce(0, -AdditionalGravity, 0, ForceMode.Force);

        if (_MoveAction.IsPressed())
        {
            _Move = _MoveAction.ReadValue<Vector2>();
            _Rigidbody.AddForce(_Move.x * Force * Time.deltaTime, 0, _Move.y * Force * Time.deltaTime, ForceMode.Impulse);
        }
        if (_JumpAction.IsPressed())
        {
            Jump();
        }
    }

    protected virtual void Jump()
    {
        if (TouchesGround())
        {
            _Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }
    }

    public virtual void WallCollision(object obj, EventArgs e)
    {
        // make player invisible and untargetable
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        // make player stop moving on all vectors
        _stopMoving = true;
        _Rigidbody.linearVelocity = Vector3.zero; 
        _Rigidbody.angularVelocity = Vector3.zero;

        // make explosion in place of player
        Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0, 0));

        // start delayed spawn
        StartCoroutine(DelaySpawn());
    }

    public virtual void SetSpawnPoint(object obj, Vector3 spawnPoint)
    {
        _SpawnPoint  = spawnPoint;
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
        
        gameObject.transform.SetPositionAndRotation(_SpawnPoint, Quaternion.Euler(-90, 0, 0)); 
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        _stopMoving = false;
    }
}
