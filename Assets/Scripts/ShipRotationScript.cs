using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipRotationScript : MonoBehaviour
{
    public float maxTiltAngle = 30f;
    public float tiltSpeed = 20f;

    private Rigidbody _rb;
    private InputAction _movmentAction; 
    private Quaternion _baseRotation;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movmentAction = InputSystem.actions.FindAction("Move");
        _baseRotation = transform.rotation;
    }

    // the new movment system makes a huge difference in here, since
    // it returns direction vector, this allows for the ship to 
    // start loosing rotation gradually when there is no input on x axis
    // also no additional if's
    void Update()
    {
        Vector2 moveDirection = _movmentAction.ReadValue<Vector2>();

        float targetTilt = Mathf.Clamp(moveDirection.x * maxTiltAngle, -maxTiltAngle, maxTiltAngle);
        Quaternion targetRotation = _baseRotation * Quaternion.Euler(0, targetTilt, 0);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }
}
