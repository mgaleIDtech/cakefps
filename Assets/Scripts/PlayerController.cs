using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Inspector Variables



    #endregion

    #region Local Members

    private PlayerMotor _playerMotor = null;

    #endregion

    private void Awake()
    {
        _playerMotor = GetComponent<PlayerMotor>();

        if (_playerMotor == null)
            Debug.LogError("Could not find a PlayerMotor component on " + gameObject.name);
    }

    private void Update()
    {
        var axisValues = new Vector2();

        axisValues = GetAxisValues();

        MovePlayer(axisValues);
    }

    private Vector2 GetAxisValues()
    {
        var axisValues = new Vector2();

        axisValues.x = Input.GetAxis("Horizontal");
        axisValues.y = Input.GetAxis("Vertical");

        return axisValues;
    }

    private void MovePlayer(Vector2 axisValues)
    {
        _playerMotor.MoveWithJoystick(axisValues);
    }
}
