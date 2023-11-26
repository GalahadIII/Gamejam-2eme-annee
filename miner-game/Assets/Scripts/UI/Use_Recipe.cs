using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Recipe : MonoBehaviour
{
    [SerializeField] Item[] itemTab;
    [SerializeField] int[] amountTab;
    [SerializeField] Item result;

    [SerializeField] internal InventoryController inventoryController;

    public void UseRecipe()
    {
        Debug.Log(itemTab[0].name);
        Debug.Log(amountTab[0]);
        if (inventoryController.inventory.ContainAmountItem(itemTab[0], amountTab[0]))
        {
            if(itemTab.Length==1)
            {
                inventoryController.inventory.AddItem(result);
                inventoryController.inventory.RemoveItem(itemTab[0], amountTab[0]);
            }
            else
            {
                if (inventoryController.inventory.ContainAmountItem(itemTab[1], amountTab[1]))
                {
                    inventoryController.inventory.RemoveItem(itemTab[0], amountTab[0]);
                    inventoryController.inventory.RemoveItem(itemTab[1], amountTab[1]);
                }
            }
        }
    }
}
