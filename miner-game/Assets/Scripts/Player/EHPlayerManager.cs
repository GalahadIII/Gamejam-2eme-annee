using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerManager2Da : MonoBehaviour, IPlayerAnimatorData2D
{

    #region IPlayerAnimatorData2D

    public Vector2 Move_Live => inputManager.PlayerInputs.Movement2d.Live;
    public bool Action1_Down => inputManager.PlayerInputs.Action1.Live;
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
    [SerializeField]
    private bool debugFindRay = true;

    [Header("Settings : Rotation")]
    [SerializeField]
    private float rotationRate = 1;
    private Quaternion rotationTarget;

    #endregion

    #region Serialize

    [Header("Serialize")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Inventory inventory;
    [SerializeField] private MusicController musicController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Pickaxe pickaxeRight;
    [SerializeField] private new Camera camera;
    private LineDrawer lineDrawer = new LineDrawer();

    #endregion

    #region DATA

    public int score = 0;
    public int hitPoint = 6;
    private bool hasControl = true;
    private Vector2 facingDirection = new Vector2(1, 0);

    #endregion

    #region PanelUI
    [Header("PannelUI")]
    [SerializeField] GameObject forgeUI;
    [SerializeField] GameObject statBarUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject settingUI;
    #endregion

    private void OnEnable()
    {
        rotationTarget = visual.localRotation;
        lineDrawer ??= new();
    }
    private void Update()
    {
        DebugTraceMineralVein();

        #region IPlayerAnimatorData2D.FacingDirection
        Vector2 newFacingDirection = inputManager.PlayerInputs.Movement2d.Live;
        if (newFacingDirection.magnitude > 0) {
            facingDirection=newFacingDirection;
        }
        switch (facingDirection.y)
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
        switch (facingDirection.x)
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
        switch (FacingDirection)
        {
            case FacingDirection4.Up:
                facingDirection = new Vector3(0, 1, 0);
                break;
            case FacingDirection4.Down:
                facingDirection = new Vector3(0, -1, 0);
                break;
            case FacingDirection4.Right:
                facingDirection = new Vector3(1, 0, 0);
                break;
            case FacingDirection4.Left:
                facingDirection = new Vector3(-1, 0, 0);
                break;
            default:
                break;
        }
        // Debug.Log($"{inputManager.PlayerInputs.Movement2d.Live} {FacingDirection}");

        #endregion

        if (inputManager.PlayerInputs.Escape.OnDown)
        {
            if (pauseUI.activeSelf || settingUI.activeSelf || forgeUI.activeSelf)
            {
                Play();
            }
            else
            {
                Pause();
            }

        }
        if (inputManager.PlayerInputs.Inventory.OnDown){
            forgeUI.SetActive(!forgeUI.activeSelf);
        }

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
        Vector2 movementInputLive = inputManager.PlayerInputs.Movement2d.Live;
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
        UsePickaxe();
    }
    private void UsePickaxe(){
        if (inputManager.PlayerInputs.Action1.Live) {
            pickaxeRight.Use();
        };
        if (inputManager.PlayerInputs.Interact.Live) {
            Vector3 worldCursorPosition = camera.ScreenToWorldPoint(inputManager.PlayerInputs.CursorPosition);
            pickaxeRight.Throw(worldCursorPosition);
        }
    }
    private Vector3 lastPickaxePosition = Vector3.zero;
    private void MovePickaxe()
    {
        // Debug.Log($"{lastPickaxePosition} {facingDirection}");
        if ((Vector2)lastPickaxePosition == facingDirection) return;
        // Debug.Log($"{lastPickaxePosition} != {facingDirection}");
        lastPickaxePosition = facingDirection;
        pickaxeRight.MoveHeldTargetDetector(facingDirection);
        // Debug.Log("UpdatePickaxePosition");

        // Vector3 position;

    }

    public void GetHit(int dmg)
    {
        hitPoint -= dmg;
        if (hitPoint <= 0)
        {
            Death();
        }
        inventoryController.UpdateDisplay();
    }

    private void Death()
    {
        Time.timeScale = 0;
        forgeUI.SetActive(false);
        statBarUI.SetActive(false);
        gameOverUI.SetActive(true);
        musicController.PutGameOverMusic();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        forgeUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    private void Play()
    {
        Time.timeScale = 1;
        forgeUI.SetActive(false);
        pauseUI.SetActive(false);
        settingUI.SetActive(false);
        statBarUI.SetActive(true);
    }

    private void DebugTraceMineralVein()
    {
        lineDrawer.Reset();

        // Debug.Log($"DebugTraceMineralVein 0 {debugFindRay} {inputManager.PlayerInputs.Action1.Live} {!debugFindRay || !inputManager.PlayerInputs.Action1.Live}");
        if (!debugFindRay || !inputManager.PlayerInputs.Action1.Live) { return; }

        // Debug.Log($"DebugTraceMineralVein 1");

        MineralVein[] mineralVeins = (MineralVein[])FindObjectsOfType(typeof(MineralVein));
        // Debug.Log(mineralVeins.Length);
        float maxDistance = float.NegativeInfinity;
        foreach (MineralVein mineralVein in mineralVeins)
        {
            Vector3 start = transform.position;
            Vector3 end = mineralVein.transform.position;
            float distance = Vector3.Distance(start, end);
            maxDistance = distance > maxDistance ? distance : maxDistance;
        }
        for (int i = 0; i < mineralVeins.Length; i++)
        {
            MineralVein mineralVein = mineralVeins[i];

            Vector3 start = transform.position;
            Vector3 end = mineralVein.transform.position;
            float distance = Vector3.Distance(start, end);
            float colorGrey = distance / maxDistance;

            lineDrawer.DrawLineInGameView(start, end, new Color(colorGrey, colorGrey, colorGrey, 1f));
            // lineRenderer.SetPosition(i * 3, )
        }
    }

}
