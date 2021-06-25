using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes input from the player and applies it to their assigned GameObject that has an ActorMotor attached to it.
/// </summary>
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    #region Inspector Variables

    /// <summary>
    /// The ActorMotor component this controller is sending data to.
    /// </summary>
    public ActorMotor TargetMotor = null;

    #endregion

    #region Local Members

    /// <summary>
    /// The button for jumping.
    /// </summary>
    private const KeyCode _jumpButton = KeyCode.Space;

    /// <summary>
    /// A Vector2 in the scope of the entire class prevents needing to create a new one constantly, helping performance.
    /// </summary>
    private Vector2 _moveVector = Vector2.zero;

    /// <summary>
    /// A deadzone helps prevent player input in the case of a malfunctioning axis.
    /// Some game controllers may constantly give off a small input on the axis even when not touched.
    /// </summary>
    private const float AXIS_DEAD_ZONE = 0.001f;

    #endregion

    #region Methods

    /// <summary>
    /// Checks if there was a valid actor assigned to the controller to prevent null reference errors from crashing code.
    /// This can be assigned directly in the inspector, or through code.
    /// </summary>
    /// <param name="showLogWarnings">Optional parameter than can be set to false if you don't want error messages.</param>
    /// <returns>True if there is a valid actor assigned.</returns>
    private bool IsValidActorAssigned(bool showLogWarnings = true)
    {
        // Send an error message to the editor so we can be reminded this needs a PlayerMotor.
        if (TargetMotor == null)
        {
            if (showLogWarnings)
                Debug.LogWarning("This is not a valid ActorMotor component assigned to the PlayerController.");
            
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Reads the player's joystick axis.  This is WASD on a keyboard, or the left joystick on a game controller.
    /// This method converts the raw data, which are floats for each axis, and combines it into an easily transferable
    /// Vector2.  Turning it into a single data structure allows for both XY data to be passed around code easily.
    /// </summary>
    /// <param name="axisValues">By using out, we allow the method to alter a pre-allocated variable, allowing for optimization by preventing allocating each check.</param>
    /// <returns>The X,Y values of the player's joystick axis.</returns>
    private bool GetAxisValues(out Vector2 axisValues)
    {
        // Read input from the player controller or WASD keys and cache them.
        axisValues.x = Input.GetAxis("Horizontal");
        axisValues.y = Input.GetAxis("Vertical");

        // Check if the deadzone requirements are met.
        return Mathf.Abs(axisValues.x) >= AXIS_DEAD_ZONE || Mathf.Abs(axisValues.y) >= AXIS_DEAD_ZONE;
    }

    /// <summary>
    /// This takes in the joystick axis data as a Vector2, and then sends it to the ActorMotor to process that movement.
    /// </summary>
    /// <param name="axisValues">The XY values of the game controller or WASD keys.</param>
    private void HandleMoveInput()
    {
        // If there is player input and a valid actor, then tell the motor to move.
        if (GetAxisValues(out _moveVector))
        {
            if (IsValidActorAssigned())
                TargetMotor.MoveWithJoystick(_moveVector);
        }
    }

    /// <summary>
    /// Sends a jump command to the ActorMotor if the relevant key is pressed.
    /// </summary>
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(_jumpButton))
        {
            if (IsValidActorAssigned())
                TargetMotor.Jump();
        }
    }

    #endregion

    /// <summary>
    /// The core logic loop of this component that runs every frame.
    /// </summary>
    private void Update()
    {
        HandleMoveInput();
        HandleJumpInput();
    }
}
