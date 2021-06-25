using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The player motor is a reusable component that can be applied to either a player or enemy to handle movement of the CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
[DisallowMultipleComponent]
public class PlayerMotor : MonoBehaviour
{
    #region Inspector Variables

    /// <summary>
    /// Movement speed of the actor.
    /// </summary>
    [SerializeField]
    private float _speed = 8f;

    /// <summary>
    /// The rotational speed of the actor.
    /// </summary>
    [SerializeField]
    private float _speedRot = 360f;

    /// <summary>
    /// The custom gravity applied to this actor.
    /// </summary>
    [SerializeField]
    private float _gravity = 15f;

    /// <summary>
    /// The amount of force applied to the initial jump action.
    /// </summary>
    [SerializeField]
    private float _jumpPower = 8f;

    /// <summary>
    /// The max number of jumps the actor can perform before landing.
    /// Note: Less than 1 will disable jumping completely.
    /// </summary>
    [SerializeField]
    private int _numberOfJumps = 2;

    #endregion

    #region Local Members

    /// <summary>
    /// The cached CharacterController attached to the same object.
    /// </summary>
    private CharacterController _cc = null;

    /// <summary>
    /// A terminal velocity for preventing too much gravity if the actor falls a long time.
    /// </summary>
    private float _maxFallSpeed { get { return _gravity; } }

    /// <summary>
    /// An upwards velocity cap for preventing too much gravity if code accidently creates an unrealistic value.
    /// </summary>
    private const float _maxRiseSpeed = 50f;

    /// <summary>
    /// A counter used to make sure the actor only jumps the specified amount of times possible.
    /// • This is reset to 0 when the actor lands.
    /// </summary>
    private int _jumpsUsed = 0;

    /// <summary>
    /// The gravity (or anti-gravity) the character is currently being applied with.
    /// </summary>
    private float _currentVertPower = 0f;

    /// <summary>
    /// A movement vector that caches the movement commands during Update() so it can be applied in FixedUpdate().
    ///     • As of Unity v2021, CharacterController.Move() seems to require FixedUpdate() like a Rigidbody would.
    /// </summary>
    private Vector3 _moveVector = Vector3.zero;

    #endregion

    #region Private Methods

    /// <summary>
    /// Awake runs only once for this component.  Use it for initializing and caching values.
    /// </summary>
    private void Awake()
    {
        // Cache the CharacterController on the same GameObject.
        _cc = GetComponent<CharacterController>();

        // If no CharacterController: Disable and show descriptive error message in console.
        if (_cc == null)
        {
            enabled = false;
            Debug.LogError("Could not find a CharacterController component on " + gameObject.name);
        }
    }

    /// <summary>
    /// Applies the cached movement vector to the CharacterController.
    /// </summary>
    private void MoveCharacterController()
    {
        // Moves the actor.
        //  • Note, this handles the Time.deltaTime, so don't need to use Time.delaTime in other movement calculations.
        //  • Try to only set .Move once per frame to prevent possible quirky bugs.
        _cc.Move(_moveVector * Time.deltaTime);

        // Reset the movement vector so no data is left over in the next update cycle.
        _moveVector = Vector3.zero;
    }

    /// <summary>
    /// Calculates gravity effect on vertical motion and caches the value in the movement vector's Y.
    /// </summary>
    private void ApplyGravityToMoveVector()
    {
        // If the actor is grounded, and not going up, then...
        if (_cc.isGrounded && _currentVertPower <= 0f)
        {
            // If _moveVector.y ever isn't negative, then isGrounded will be false.
            _currentVertPower = -_gravity * Time.deltaTime;

            // Reset jumps used because the actor is grounded.
            _jumpsUsed = 0;
        }
        else
        {
            // This runs when the actor is airborne, either while jumping or falling.
            _currentVertPower -= _gravity * Time.deltaTime;
            _currentVertPower = Mathf.Clamp(_currentVertPower, -_maxFallSpeed, _maxRiseSpeed);
        }

        // Cache the calculated vertical movement direction in the movement vector...
        //      so it can be applied to the CharacterController in another method.
        _moveVector.y = _currentVertPower;
    }

    /// <summary>
    /// Checks if the actor can jump based on conditions.
    /// </summary>
    /// <returns>True if it can.</returns>
    private bool CanJump()
    {
        return _jumpsUsed < _numberOfJumps;
    }

    /// <summary>
    /// Checks if this component is enabled, and if not will throw an error message.
    /// </summary>
    /// <param name="postErrorMessage">If true then will post any errors to the console.</param>
    /// <returns>True if the component is enabled.</returns>
    private bool IsEnabled(bool postErrorMessage = true)
    {
        if (enabled)
        {
            return true;
        }
        else
        {
            Debug.LogWarning(string.Format("ActorMotor attempted to move, but the component is disabled on GameObject: {0}", gameObject.name));
            return false;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Interprets a Vector2 as the XY axis on a player joystick and converts it to the proper movement axis for the actor's personal directions.
    /// </summary>
    /// <param name="joystickAxis">The XY values of the joystick.</param>
    public void MoveWithJoystick(Vector2 joystickAxis)
    {
        if (!IsEnabled()) return; // Don't execute any code if the component isn't enabled.

        // Move (Vertical (Y) is forward and backwards.)
        //  • TransformDirection is used to get the forward of this actor, instead of just the forward of world space.
        _moveVector = transform.TransformDirection(Vector3.forward) * (joystickAxis.y * _speed);

        // Rotation (Horizontal (X) is rotating)
        //  • Unity transforms use Quaternions on the backend, so convert to euler to edit them.
        //  • Inspector values are actually Euler and not Quaternion.
        var rot = transform.rotation.eulerAngles;
        rot.y += joystickAxis.x * _speedRot * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rot);
    }

    /// <summary>
    /// When invoked, this method will attempt to jump.
    /// By default: If the actor is already jumping, this will ignore the jump command.
    /// </summary>
    /// <param name="forceJump">Set to true to ignore if the player is jumping already</param>
    public void Jump(bool forceJump = false)
    {
        if (!IsEnabled()) return; // Don't execute any code if the component isn't enabled.

        if (CanJump() || forceJump)
        {
            _jumpsUsed++; // Increment the number of jumps used (resets when the actor lands).
            _currentVertPower = _jumpPower; // Set the vertical velocity so the player going up.
        }
    }

    #endregion

    /// <summary>
    /// The core logic loop of this component that runs every frame.
    ///     • FixedUpdate() is like Update(), but is used specifically for physics objects.
    ///     • If FixedUpdate() is not used, you will get erroneous results when using Rigidbody or CharacterController.
    /// </summary>
    private void FixedUpdate()
    {
        ApplyGravityToMoveVector();
        MoveCharacterController();
    }
}
