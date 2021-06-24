using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _cc = null;

    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _speedRot = 90f;
    [SerializeField]
    private float _gravity = 2f;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();

        if (_cc == null)
            Debug.LogError("Could not find a CharacterController component on " + gameObject.name);
    }

    private void Update()
    {
        if (_cc.isGrounded == false)
            ApplyGravity();
    }

    private void ApplyGravity()
    {
        _cc.Move(new Vector3(0f, -_gravity * Time.deltaTime, 0f));
    }

    public void MoveWithJoystick(Vector2 joystickAxis)
    {
        Vector3 moveVector = new Vector3();

        // Vertical (Y) is forward and backwards.
        moveVector = transform.TransformDirection(Vector3.forward) * (joystickAxis.y * _speed * Time.deltaTime);
        _cc.Move(moveVector);

        // Rotation
        // Horizontal (X) is rotating
        var rot = transform.rotation.eulerAngles;
        rot.y += joystickAxis.x * _speedRot * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rot);

        Debug.Log("Rot: " + rot);
    }
}
