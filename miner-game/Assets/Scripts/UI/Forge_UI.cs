using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Forge_UI : MonoBehaviour
{
    [SerializeField] InventoryController inventoryController;

    [Header("Inventory storage")]
    [SerializeField] TextMeshProUGUI tmpCoal;
    [SerializeField] TextMeshProUGUI tmpIron;
    [SerializeField] TextMeshProUGUI tmpGold;
    [SerializeField] TextMeshProUGUI tmpDiamond;

    internal void UpdateStorage()
    {
        tmpCoal.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[0]).ToString();
        tmpIron.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[1]).ToString();
        tmpGold.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[2]).ToString();
        tmpDiamond.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[3]).ToString();
    }

}
