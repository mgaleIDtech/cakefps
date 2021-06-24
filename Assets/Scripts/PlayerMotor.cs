using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _cc = null;

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
        _cc.Move(new Vector3(0f, -_gravity, 0f));
    }

    public void MoveWithJoystick(Vector2 joystickAxis)
    {
        Vector3 moveVector = new Vector3();

        //moveVector.x = 

        //_cc.Move(moveVector);
    }
}
