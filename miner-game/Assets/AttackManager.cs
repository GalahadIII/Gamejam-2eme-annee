using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [Header("Serialize")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Pickaxe weapon;

    [Header("Settings")]
    [SerializeField] private float durationAttack = 0.3f;
    [SerializeField] private float cooldownAttack = 0.8f;
    private float lastAttackTime = 0;

    private void OnEnable()
    {
        lastAttackTime = -cooldownAttack;
    }

    private void FixedUpdate()
    {
        if (!inputManager.PlayerInputs.Interact.Live) return;

        if (lastAttackTime + cooldownAttack > Time.time) return;
        lastAttackTime = Time.time;

    }
    public void Attack()
    {
        weapon.IWeapon_Use();
    }
}
