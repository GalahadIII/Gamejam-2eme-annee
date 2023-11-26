using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject Player;
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
        stat_UI.UpdateHitPointDisplay(5);
        stat_UI.UpdateScoreDisplay(2800);
        stat_UI.UpdateInventoryDisplay();
    }
}
