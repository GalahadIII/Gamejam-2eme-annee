using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pickaxe : ObjectColliderDetector2D
{

    [Header("Pickaxe properties")]
    [SerializeField] private float efficiency = 1f;
    [SerializeField] private float cooldown = 1f;

    [Header("Data")]
    private float lastUse = 0;

    public void Use()
    {
        if (Time.time - lastUse < cooldown) return;
        // Debug.Log($"Pickaxe Use 0");

        GameObject o = Objects.FirstOrDefault(o =>
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

}

struct PickaxeSettings
{
    public float Efficiency;
    public float Cooldown;
}
