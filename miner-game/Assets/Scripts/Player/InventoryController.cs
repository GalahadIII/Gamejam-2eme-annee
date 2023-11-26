using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] internal GameObject player;
    [SerializeField] internal Forge_UI forge_UI;
    [SerializeField] internal Stat_UI stat_UI;

    [SerializeField] internal Inventory inventory;

    private void Start()
    {
        UpdateDisplay();
    }
    internal void UpdateDisplay()
    {
        forge_UI.UpdateStorage();
        stat_UI.UpdateHitPointDisplay();
        stat_UI.UpdateScoreDisplay();
        stat_UI.UpdateInventoryDisplay();
    }
}
