using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerManager2D : MonoBehaviour, IPlayerAnimatorData2D
{

    #region IPlayerAnimatorData2D

    public Vector2 Move_Live => input.PlayerInputs.Movement2d.Live;
    public bool Action1_Down => input.PlayerInputs.Action1.Live;
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

    [Header("Settings : Visual")]
    [SerializeField]
    private Transform visual;

    [Header("Settings : Rotation")]
    [SerializeField]
    private float rotationRate = 1;
    private Quaternion rotationTarget;

    #endregion

    #region GetComponent'ed

    [SerializeField] InventoryController inventoryController;
    private Rigidbody2D rb;
    private InputManager input;
    public Pickaxe pickaxeRight;
    public int score = 0;
    public int hitPoint = 6;

    #endregion

    #region DATA

    private bool hasControl = true;
    private Vector2 facingDirection = new Vector2(1, 0);

    #endregion

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputManager>();
        pickaxeRight = GetComponentInChildren<Pickaxe>();
        rotationTarget = visual.localRotation;
    }
    private void Update()
    {
        #region IPlayerAnimatorData2D.FacingDirection

        switch (input.PlayerInputs.Movement2d.Live.y)
        {
            case > 0.1f:
                FacingDirection = FacingDirection4.Up;
                break;
            case < -0.1f:
                FacingDirection = FacingDirection4.Down;
                break;
            default:
                break;
        }
        switch (input.PlayerInputs.Movement2d.Live.x)
        {
            case > 0.1f:
                FacingDirection = FacingDirection4.Right;
                break;
            case < -0.1f:
                FacingDirection = FacingDirection4.Left;
                break;
            default:
                break;
        }
        // Debug.Log($"{input.PlayerInputs.Movement2d.Live} {FacingDirection}");

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
        // FlipX(); // Unused
        Movement2d();
        HandlePickaxe();
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

    private void HandlePickaxe()
    {
        MovePickaxe();
        switch (input.PlayerInputs.Action1.Live)
        {
            case true:
                pickaxeRight.Use();
                break;
            default:
                break;
        }
    }
    private FacingDirection4 pickaxePosition = FacingDirection4.Right;
    private void MovePickaxe()
    {
        if (pickaxePosition == FacingDirection) return;
        // Debug.Log("UpdatePickaxePosition");

        Vector3 position = pickaxeRight.transform.localPosition;
        switch (FacingDirection)
        {
            case FacingDirection4.Up:
                position = new Vector3(0, 1, 0);
                break;
            case FacingDirection4.Down:
                position = new Vector3(0, -1, 0);
                break;
            case FacingDirection4.Right:
                position = new Vector3(1, 0, 0);
                break;
            case FacingDirection4.Left:
                position = new Vector3(-1, 0, 0);
                break;
            default:
                break;
        }
        pickaxePosition = FacingDirection;
        pickaxeRight.transform.localPosition = position;

    }

    public void GetHit(int dmg)
    {
        hitPoint -= dmg;
        inventoryController.UpdateDisplay();
    }

}
