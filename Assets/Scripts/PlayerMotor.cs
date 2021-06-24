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
    [SerializeField]
    private float _jumpPower = 2f;

    /// <summary>
    /// The gravity (or anti-gravity) the character is currently being applied with.
    /// </summary>
    private float _currentVertPower = 0f;

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
        if (_cc.isGrounded) return;

        _currentVertPower -= _gravity * Time.deltaTime;

        _cc.Move(new Vector3(0f, _currentVertPower * Time.deltaTime, 0f));
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
    }

    public void Jump()
    {
        _currentVertPower = _jumpPower;
    }
}
