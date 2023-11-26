using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Recipe : MonoBehaviour
{
    [SerializeField] Item[] itemTab;
    [SerializeField] int[] amountTab;
    [SerializeField] Item result;
    [SerializeField] int result_levelPickaxe;

    [SerializeField] internal InventoryController inventoryController;

    public void UseRecipe()
    {
        if (inventoryController.inventory.ContainAmountItem(itemTab[0], amountTab[0]))
        {
            inventoryController.inventory.AddItem(result);
            inventoryController.inventory.RemoveItem(itemTab[0], amountTab[0]);
        }
    }

    public void UpgradePickaxe()
    {
        if (inventoryController.inventory.ContainAmountItem(itemTab[0], amountTab[0]) &&
            inventoryController.inventory.ContainAmountItem(itemTab[1], amountTab[1]) &&
            result_levelPickaxe> inventoryController.player.GetComponent<PlayerManager2D>().pickaxeRight.Level)
        {
            inventoryController.player.GetComponent<PlayerManager2D>().pickaxeRight.Level = result_levelPickaxe;
            inventoryController.inventory.RemoveItem(itemTab[0], amountTab[0]);
            inventoryController.inventory.RemoveItem(itemTab[1], amountTab[1]);
        }
    }
}
