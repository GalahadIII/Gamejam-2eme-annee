using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pickaxe : MonoBehaviour
{

    [Header("Pickaxe properties")]
    [SerializeField] private float efficiency = 1f;
    [SerializeField] private float cooldown = 1f;

    [Header("Data")]
    private float lastUse = 0;
    private ObjectColliderDetector2D detector;

    private void OnEnable()
    {
        detector = GetComponent<ObjectColliderDetector2D>();
    }

    public void Use()
    {
        if (Time.time - lastUse < cooldown) return;
        // Debug.Log($"Pickaxe Use 0");

        GameObject o = detector.Objects.FirstOrDefault(o =>
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
