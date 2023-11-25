using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerManager2D : MonoBehaviour, IPlayerManager2D
{
    #region IPlayerManager2D
    public Vector2 Move_Live => frameInput.Movement2d.Live;
    public bool Action1_FixedOnDown => frameInput.Action1.FixedOnDown;
    public bool Rigidbody2D => rb;
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
    private bool flipXUsingScale = true;
    [SerializeField]
    private float flipXRotationRate = 0;
    private bool flipXRotationRateIsRelativeToDeltaTime = false;
    private Quaternion flipXRotationDefault;
    private Quaternion flipXRotationFinal;

    [Header("Settings : Rotation visual settings")]
    private Quaternion rotationTarget;
    private bool rotationRateIsRelativeToDeltaTime = false;
    private float rotationRate = 1;

    #endregion

    #region INST

    private Rigidbody2D rb;
    private InputManager input;
    private PlayerInputs frameInput;

    #endregion

    private bool hasControl;
    private Vector2 facingDirection = new(1, 0);

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
    }
    private void FixedUpdate()
    {
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
        Vector2 input = frameInput.Movement2d.Live;
        Vector2 vel = rb.velocity;

        // calculate wanted direction and desired velocity
        Vector2 targetSpeed = input * moveSpeed;
        // calculate difference between current velocity and target velocity
        Vector2 speedDif = targetSpeed - vel;
        // change acceleration rate depending on situations
        float accelRate = input.magnitude > 0.01f ? acceleration : decceleration;

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
        float inputMovementX = frameInput.Movement2d.Live.x;

        // if we dont have movement, exit
        if (Mathf.Abs(inputMovementX) < 0.1f) return;
        // if we are facing +x and movement goes +x
        if (facingDirection.x > 0 && inputMovementX > 0) return;
        // if we are facing -x and movement goes -x
        if (facingDirection.x < 0 && inputMovementX < 0) return;

        if (flipXUsingScale)
        {
            FlipX_Scale();
        }
        else
        {
            FlipX_Rotation(inputMovementX);
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
        if (visual.rotation == rotationTarget) return;

        float finalRotationRate = rotationRate;
        switch (rotationRateIsRelativeToDeltaTime)
        {
            case true:
                finalRotationRate *= Time.deltaTime;
                break;
            default:
                break;
        }
        visual.rotation = Quaternion.Slerp(visual.rotation, rotationTarget, finalRotationRate);
    }

}

public interface IPlayerManager2D
{
    public Vector2 Move_Live { get; }
    public bool Action1_FixedOnDown { get; }
    public bool Rigidbody2D { get; }
}
