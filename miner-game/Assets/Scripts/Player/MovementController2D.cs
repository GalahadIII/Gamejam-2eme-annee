using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputManager))]
public class MovementController2D : MonoBehaviour
{

    #region SETTINGS

    [SerializeField]
    private float moveSpeed = 20f;

    [SerializeField]
    private float acceleration = 6f;

    [SerializeField]
    private float decceleration = 6f;

    [SerializeField]
    private float velPower = 1.2f;

    #endregion

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Rigidbody2D _rb;

    private PlayerInputs _frameInput;

    private bool _facingPositiveX;

    public bool DisabledControls = false;

    private void OnEnable() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _frameInput = inputManager.PlayerInputs;
    }

    private void FixedUpdate() {
        // Debug.Log(_frameInput.Movement2d.Live);
        if (DisabledControls) return;
        Movement2d();
    }

    private void Movement2d() {
        Vector2 input = _frameInput.Movement2d.Live;
        Vector2 vel = _rb.velocity;

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
        _rb.AddForce(movement, ForceMode2D.Force);
        // Debug.Log($"{input} / {movement} / {_rb.velocity}");
    }

    public void ResetVelocity() {
        _rb.velocity = Vector3.zero;
    }
}
