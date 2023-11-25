using System;
using System.Collections.Generic;
using Unity.Mathematics;
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

        rotationTarget = visual.localRotation;
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
        float inputMovementX = inputMovement.x;

        Debug.Log($"face:{facingDirection.x} move.x:{inputMovementX}");

        // EXIT IF:
        // we dont have any movement input
        if (Mathf.Abs(inputMovementX) < 0.1f) return;
        // we have same input sign and currently facing sign (+ == + || - == -)
        if (Mathf.Sign(facingDirection.x) == Mathf.Sign(inputMovementX)) return;

        Debug.LogWarning($"face:{facingDirection.x} move.x:{inputMovementX}");

        switch (flipXUsingRotation)
        {
            case true:
                FlipX_Rotation();
                break;
            default:
                FlipX_Scale();
                break;
        }

        // update currently known facing direction
        facingDirection = inputMovement;

    }
    private void FlipX_Scale()
    {
        Vector3 newScale = visual.localScale;
        newScale.x *= -1;
        visual.localScale = newScale;
        // visual.localScale *= new Vector3(1, -1, 1);
    }
    private void FlipX_Rotation()
    {
        Quaternion RotationDiff = Quaternion.Euler(0, 180, 0);
        visual.localRotation *= RotationDiff;
        // Quaternion newRot = visual.localRotation;
        // newRot *= RotationDiff;
        // visual.localRotation = newRot;
        // RotateOverTime(newRot, flipXRotationRate, flipXRotationRateIsRelativeToDeltaTime);
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
