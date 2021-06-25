using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _cc = null;

    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _speedRot = 360f;
    [SerializeField]
    private float _gravity = 15f;
    private const float _maxRiseSpeed = 50f;
    [SerializeField]
    private float _jumpPower = 8f;
    private float _maxFallSpeed { get { return _gravity; } }

    /// <summary>
    /// The gravity (or anti-gravity) the character is currently being applied with.
    /// </summary>
    private float _currentVertPower = 0f;

    Vector3 _moveVector = Vector3.zero;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();

        if (_cc == null)
            Debug.LogError("Could not find a CharacterController component on " + gameObject.name);
    }

    private void FixedUpdate()
    {
        Debug.Log("_cc.isGrounded: " + _cc.isGrounded);
        ApplyGravityToMoveVector();
        ApplyMoveVector();
    }

    private void ApplyMoveVector()
    {
        if (_moveVector.y == 0f)
            Debug.Log("_moveVector: " + _moveVector);
        _cc.Move(_moveVector * Time.deltaTime);
        _moveVector = Vector3.zero;
    }

    private void ApplyGravityToMoveVector()
    {
        if (_cc.isGrounded && _currentVertPower <= 0f)
        {
            // If _moveVector.y ever isn't negative, then isGrounded will be false.
            _currentVertPower = -_gravity * Time.deltaTime;
        }
        else
        {
            _currentVertPower -= _gravity * Time.deltaTime;
            _currentVertPower = Mathf.Clamp(_currentVertPower, -_maxFallSpeed, _maxRiseSpeed);
        }

        _moveVector.y = _currentVertPower;
    }

    public void MoveWithJoystick(Vector2 joystickAxis)
    {
        // Move
        // Vertical (Y) is forward and backwards.
        _moveVector = transform.TransformDirection(Vector3.forward) * (joystickAxis.y * _speed);

        // Rotation
        // Horizontal (X) is rotating
        var rot = transform.rotation.eulerAngles;
        rot.y += joystickAxis.x * _speedRot * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rot);
    }

    public void Jump()
    {
        if (_cc.isGrounded)
        {
            _currentVertPower = _jumpPower;
        }
    }
}
