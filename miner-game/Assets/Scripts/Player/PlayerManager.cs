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

    [Header("Serialize")]
    [SerializeField]
    private Transform visual;

    #endregion

    #region INST

    private Rigidbody2D rb;
    private InputManager input;
    private PlayerInputs frameInput;

    #endregion

    private bool hasControl;
    private bool facingPositiveX;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
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
        Vector2 input = frameInput.Movement2d.Live;
        if (Mathf.Abs(input.x) < 0.1f)
            return;
        if (facingPositiveX && Mathf.Sign(input.x) < 0)
            return;
        if (!facingPositiveX && Mathf.Sign(input.x) > 0)
            return;

        Vector3 newScale = transform.localScale;
        if (Mathf.Sign(input.x) == Mathf.Sign(newScale.x))
            newScale.x *= facingPositiveX ? 1 : -1;
        transform.localScale = newScale;

        facingPositiveX = !facingPositiveX;
    }
}

public interface IPlayerManager2D
{
    public Vector2 Move_Live { get; }
    public bool Action1_FixedOnDown { get; }
    public bool Rigidbody2D { get; }
}
