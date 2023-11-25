using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerManager2D : MonoBehaviour, IPlayerAnimatorData2D
{

    #region IPlayerAnimatorData2D

    public Vector2 Move_Live => input.PlayerInputs.Movement2d.Live;
    public bool Action1_FixedOnDown => input.PlayerInputs.Action1.FixedOnDown;
    public Rigidbody2D Rigidbody2D => rb;
    public FacingDirection4 FacingDirection { get; private set; }

    #endregion

    #region SETTINGS

    [Header("Settings : Movement 2d (left,right,up,down)")]
    [SerializeField]
    private float moveSpeed = 20f;

    [SerializeField]
    private float acceleration = 6f;

    [SerializeField]
    private float decceleration = 6f;

    [SerializeField]
    private float velPower = 1.2f;

    [Header("Settings : Visual facing flip")]
    [SerializeField]
    private Transform visual;
    [SerializeField]
    private bool flipXUsingRotation = true;
    [SerializeField]
    private float flipXRotationRate = 0;
    [SerializeField]
    private bool flipXRotationRateIsRelativeToDeltaTime = false;
    [SerializeField]
    private Quaternion flipXRotationDefault = Quaternion.identity;
    [SerializeField]
    private Quaternion flipXRotationFinal = Quaternion.identity;

    [Header("Settings : Rotation visual settings")]
    [SerializeField]
    private bool rotationRateIsRelativeToDeltaTime = false;
    [SerializeField]
    private float rotationRate = 1;
    private Quaternion rotationTarget;

    #endregion

    #region GetComponent'ed

    private Rigidbody2D rb;
    private InputManager input;

    #endregion

    #region DATA

    private bool hasControl = true;
    private Vector2 facingDirection = new Vector2(1, 0).normalized;

    #endregion

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputManager>();

        flipXRotationDefault *= visual.rotation;
        flipXRotationFinal *= visual.rotation;
    }
    private void Update()
    {
        Rotate();

        #region IPlayerAnimatorData2D.FacingDirection

        switch (facingDirection.x)
        {
            case > 0.1f:
                FacingDirection = FacingDirection4.Right;
                break;
            case < 0.1f:
                FacingDirection = FacingDirection4.Left;
                break;
            default:
                break;
        }
        switch (facingDirection.y)
        {
            case > 0.1f:
                FacingDirection = FacingDirection4.Up;
                break;
            case < 0.1f:
                FacingDirection = FacingDirection4.Down;
                break;
            default:
                break;
        }

        #endregion

    }
    private void FixedUpdate()
    {
        // if (rb.velocity.magnitude < 0.01f)
        // {
        //     rb.velocity = Vector2.zero;
        // }

        switch (hasControl)
        {
            case false:
                return;
            default:
                break;
        }
        FlipX();
        Movement2d();
    }

    private void Movement2d()
    {
        Vector2 movementInputLive = input.PlayerInputs.Movement2d.Live;
        Vector2 vel = rb.velocity;

        // calculate wanted direction and desired velocity
        Vector2 targetSpeed = movementInputLive * moveSpeed;
        // calculate difference between current velocity and target velocity
        Vector2 speedDif = targetSpeed - vel;
        // change acceleration rate depending on situations
        float accelRate = movementInputLive.magnitude > 0.01f ? acceleration : decceleration;

        // applies acceleration to speed difference, raise to a set power so acceleration increase with higher speed
        // multiply by sign to reapply direction
        float movementX = Mathf.Pow(Mathf.Abs(speedDif.x) * accelRate, velPower) * Mathf.Sign(speedDif.x);
        float movementY = Mathf.Pow(Mathf.Abs(speedDif.y) * accelRate, velPower) * Mathf.Sign(speedDif.y);
        Vector2 movement = new(movementX, movementY);

        // apply the movement force
        rb.AddForce(movement, ForceMode2D.Force);
        // Debug.Log($"{input} / {movement} / {_rb.velocity}");
    }

    private void FlipX()
    {
        Vector2 inputMovement = input.PlayerInputs.Movement2d.Live;
        facingDirection = inputMovement;
        float inputMovementX = inputMovement.x;
        Debug.Log($"face:{facingDirection.x} move.x:{inputMovementX} move:{input.PlayerInputs.Movement2d.Live}");

        // if we dont have movement, exit
        if (Mathf.Abs(inputMovementX) < 0.1f) return;
        // if we are facing +x and movement goes +x
        if (facingDirection.x > 0 && inputMovementX > 0) return;
        // if we are facing -x and movement goes -x
        if (facingDirection.x < 0 && inputMovementX < 0) return;

        if (flipXUsingRotation)
        {
            FlipX_Rotation(inputMovementX);
        }
        else
        {
            FlipX_Scale();
        }

        // update currently known facing direction
        facingDirection.x = inputMovementX;

    }
    private void FlipX_Scale()
    {
        Vector3 newScale = visual.localScale;
        newScale.x *= -1;
        visual.localScale = newScale;
    }
    private void FlipX_Rotation(float signInputMovementX)
    {
        Quaternion newRot = visual.localRotation;
        newRot *= Quaternion.Euler(0, 180 * signInputMovementX, 0);
        RotateOverTime(newRot, flipXRotationRate, flipXRotationRateIsRelativeToDeltaTime);
    }

    public void RotateOverTime(Quaternion newRotationTarget, float newRotationRate, bool newRotationRateIsRelativeToDeltaTime)
    {
        rotationTarget = newRotationTarget;
        rotationRate = newRotationRate;
        rotationRateIsRelativeToDeltaTime = newRotationRateIsRelativeToDeltaTime;
    }
    private void Rotate()
    {
        if (visual.localRotation == rotationTarget) return;

        // Debug.Log($"{visual.localRotation.eulerAngles} {rotationTarget.eulerAngles}");

        float finalRotationRate = rotationRate;
        switch (rotationRateIsRelativeToDeltaTime)
        {
            case true:
                finalRotationRate *= Time.deltaTime;
                break;
            default:
                break;
        }
        visual.localRotation = Quaternion.Slerp(visual.localRotation, rotationTarget, finalRotationRate);
    }

}
