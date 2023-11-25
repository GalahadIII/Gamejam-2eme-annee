using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(MovementController2D))]
public class PlayerManager : MonoBehaviour
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

#region SERIAL

#endregion

#region INST

    private Rigidbody2D _rb;
    private InputManager _input;
    private PlayerInputs _frameInput;

#endregion

    #region Private

    [SerializeField] private LayerMask _groundLayerMask;


    private int _fixedUpdateCounter;
    private bool _hasControl;
    private bool _grounded;
    private bool _facingPositiveX;
    private float _gravityScale;
    private BoxCollider2D _feetCollider;
    private CapsuleCollider2D _headCollider;

    #endregion

    #region Public

    public Vector2 Input => _frameInput.Movement2d.Live;
    public Vector2 Speed => _rb.velocity;
    public bool Falling => _rb.velocity.y < 0;
    public bool Jumping => _rb.velocity.y > 0;
    public bool FacingPositiveX => _facingPositiveX;
    public bool Grounded => _grounded;
    public bool Rolling => _rolling;
    public bool Dashing => _dashing;

    #endregion

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputManager>();
        _feetCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        _fixedUpdateCounter++;

        // input dependant
        Flip();
        Horizontal();
        // Dodge();
        ArtificialFriction();
    }

    #region Horizontal

    private void Horizontal()
    {
        if (_dashing || _rolling) return;

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
        Vector2 movement = new (movementX, movementY);

        // apply the movement force
        _rb.AddForce(movement, ForceMode2D.Force);
        Debug.Log($"{input} / {movement} / {_rb.velocity}");
    }

    #endregion

    private void FlipX()
    {
        if (Mathf.Abs(Input.x) < 0.1f) return;
        if (_facingPositiveX && Mathf.Sign(Input.x) < 0) return;
        if (!_facingPositiveX && Mathf.Sign(Input.x) > 0) return;

        Vector3 newScale = transform.localScale;
        if (Mathf.Sign(Input.x) == Mathf.Sign(newScale.x))

        newScale.x *= _facingPositiveX ? 1 : -1;
        transform.localScale = newScale;

        _facingPositiveX = !_facingPositiveX;
    }
}

public interface IPlayerController
{
    public Vector2 Input { get; }
    public Vector2 Speed { get; }
    public bool Falling { get; }
    public bool Jumping { get; }
    public bool Rolling { get; }
    public bool Dashing { get; }
    public bool Grounded { get; }
}