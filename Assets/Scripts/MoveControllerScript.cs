using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float JumpForce = 2f;
    public float Force = 10;
    public float AdditionalGravity = 2;
    
    
    private InputAction _MoveAction;
    private InputAction _JumpAction;
    private Vector3 _Move;
    private Vector3 _SpawnPoint;

    void Start()
    {
        // actions
        _MoveAction = InputSystem.actions.FindAction("Move");
        _JumpAction = InputSystem.actions.FindAction("Jump");

        // variable assigment
        Rigidbody = GetComponent<Rigidbody>();

        // events
        WallsScript.e_WallCollision += WallCollision;
        SpawnPointScript.e_SetSpawn += SetSpawnPoint;
        _SpawnPoint = new Vector3(0, 0.5f, 0);
    }

    private void OnDestroy()
    {
        // this fixes error on scene change
        WallsScript.e_WallCollision -= WallCollision;
        SpawnPointScript.e_SetSpawn -= SetSpawnPoint;
    }

    void FixedUpdate()
    {
        Rigidbody.AddForce(0, -AdditionalGravity, 0, ForceMode.Force);

        if (_MoveAction.IsPressed())
        {
            _Move = _MoveAction.ReadValue<Vector2>();
            Rigidbody.AddForce(_Move.x * Force * Time.deltaTime, 0, _Move.y * Force * Time.deltaTime, ForceMode.Impulse);
        }
        if (_JumpAction.IsPressed())
        {
            Jump();
        }
    }

    void Jump()
    {
        if (TouchesGround())
        {
            Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }
    }

    public void WallCollision(object obj, GameObject go)
    {
        gameObject.transform.SetPositionAndRotation(_SpawnPoint, Quaternion.Euler(-90, 0, 0));
    }

    public void SetSpawnPoint(object obj, Vector3 spawnPoint)
    {
        _SpawnPoint  = spawnPoint;
    }

    public bool TouchesGround()
    {
        // checking if there is ground underneath using raycast,
        // better than managing it from perspective of ground
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    
}
