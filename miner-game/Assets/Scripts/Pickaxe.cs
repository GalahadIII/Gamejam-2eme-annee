using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickaxe : MonoBehaviour, ITool, IWeapon
{
    [Header("Serialize")]
    [SerializeField] private ObjectColliderDetector detectorDirect;
    [SerializeField] private ObjectColliderDetector detectorThrown;

    [Header("Pickaxe properties")]
    [SerializeField] private float throwRange = 5f;
    [SerializeField] private float damageHeld = 1f;
    [SerializeField] private float damageThrown = 1f;

    [SerializeField] private int efficiency = 1;
    [SerializeField] private float cooldown = 1f;
    // [SerializeField] int level = 1;

    // public
    public int Level { get { return efficiency; } set { efficiency = value; } }

    private bool held = true;


    [Header("Data")]
    private float lastUse = 0;

    private void FixedUpdate()
    {

    }

    public void DealThrowDamage()
    {
        if (held) return;


    }

    public void ThrowTowards(Vector3 targetWorldPosition)
    {
        Vector2 direction = (transform.position - targetWorldPosition).normalized;
        Debug.Log($"Pickaxe ThrowTowards {targetWorldPosition} /// {direction}");
    }

    public void Use()
    {
        if (Time.time - lastUse < cooldown) return;
        // Debug.Log($"Pickaxe Use 0");

        GameObject o = GetObjectsInRange().FirstOrDefault(o =>
        {
            o.TryGetComponent(out MineralVein mineralVein);
            return mineralVein != null;
        });
        if (o == null) return;
        // Debug.Log($"Pickaxe Use 1");

        o.TryGetComponent(out MineralVein mineralVein);
        if (mineralVein == null) return;

        // Debug.Log($"Pickaxe Use 2");

        mineralVein.Mine(efficiency);
        lastUse = Time.time;
    }

    public void Attack()
    {

    }

    public void Throw(Vector3 targetPosition)
    {

    }

    private List<GameObject> GetObjectsInRange()
    {
        if (held)
        {
            return detectorDirect.Objects;
        }
        else
        {
            return detectorThrown.Objects;
        }
    }

    public Vector3 HeldDetectorPosition => detectorDirect.transform.localPosition;
    public void MoveHeldTargetDetector(Vector3 offsetPosition)
    {
        detectorDirect.transform.localPosition = offsetPosition;
    }

    public void ITool_Use()
    {
        Use();
    }

    public void IWeapon_Use()
    {
        Attack();
    }
}

struct PickaxeSettings
{
    public float Efficiency;
    public float Cooldown;
}
