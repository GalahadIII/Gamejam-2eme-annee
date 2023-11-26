using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Recipe : MonoBehaviour
{
    [SerializeField] Item[] itemTab;
    [SerializeField] int[] amountTab;
    [SerializeField] Item result;
    [SerializeField] int result_levelPickaxe;

    [SerializeField] internal Forge_UI forge_UI;
    [SerializeField] internal InventoryController InventoryController => forge_UI.InventoryController;

    public void UseRecipe()
    {
        if (InventoryController.inventory.ContainAmountItem(itemTab[0], amountTab[0]))
        {
            InventoryController.inventory.AddItem(result);
            InventoryController.inventory.RemoveItem(itemTab[0], amountTab[0]);
        }
    }

    public void UpgradePickaxe()
    {
        if (InventoryController.inventory.ContainAmountItem(itemTab[0], amountTab[0]) &&
            InventoryController.inventory.ContainAmountItem(itemTab[1], amountTab[1]) &&
            result_levelPickaxe> InventoryController.player.GetComponent<PlayerManager2D>().pickaxeRight.Level)
        {
            InventoryController.player.GetComponent<PlayerManager2D>().pickaxeRight.Level = result_levelPickaxe;
            InventoryController.player.GetComponent<PlayerManager2D>().score += result.scoreGiven;
            InventoryController.inventory.RemoveItem(itemTab[0], amountTab[0]);
            InventoryController.inventory.RemoveItem(itemTab[1], amountTab[1]);
        }
    }
}
