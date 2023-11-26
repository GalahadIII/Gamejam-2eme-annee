using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Forge_UI : MonoBehaviour
{
    [SerializeField] public InventoryController InventoryController;

    [Header("Inventory storage")]
    [SerializeField] TextMeshProUGUI tmpCoal;
    [SerializeField] TextMeshProUGUI tmpIron;
    [SerializeField] TextMeshProUGUI tmpGold;
    [SerializeField] TextMeshProUGUI tmpDiamond;

    internal void UpdateStorage()
    {
        tmpCoal.text = InventoryController.inventory.GetAmountItem(InventoryController.inventory.listItem[0]).ToString();
        tmpIron.text = InventoryController.inventory.GetAmountItem(InventoryController.inventory.listItem[1]).ToString();
        tmpGold.text = InventoryController.inventory.GetAmountItem(InventoryController.inventory.listItem[2]).ToString();
        tmpDiamond.text = InventoryController.inventory.GetAmountItem(InventoryController.inventory.listItem[3]).ToString();
    }

}
