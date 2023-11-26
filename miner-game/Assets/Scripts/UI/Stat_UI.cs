using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat_UI : MonoBehaviour
{
    [SerializeField] PlayerManager2D player;
    [SerializeField] InventoryController inventoryController;

    [Header("HitPoint")]
    [SerializeField] Image hitPointImage;
    [SerializeField] Sprite[] hitPointspriteList;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI tmpScore;
    [SerializeField] TextMeshProUGUI tmpScoreGameOver;

    [Header("Inventory")]
    [SerializeField] Image pickaxe;
    [SerializeField] Sprite[] pickaxeSpriteList;
    [SerializeField] TextMeshProUGUI tmpTorch;
    [SerializeField] TextMeshProUGUI tmpCoal;
    [SerializeField] TextMeshProUGUI tmpIron;
    [SerializeField] TextMeshProUGUI tmpGold;
    [SerializeField] TextMeshProUGUI tmpDiamond;

    internal void UpdateHitPointDisplay()
    {
        hitPointImage.sprite = hitPointspriteList[player.hitPoint];
    }

    internal void UpdateScoreDisplay()
    {
        tmpScore.text = player.score.ToString();
        tmpScoreGameOver.text = player.score.ToString();
    }

    internal void UpdateInventoryDisplay()
    {
        int pickaxeLvl = player.pickaxeRight.Level;
        pickaxe.sprite = pickaxeSpriteList[pickaxeLvl-1];
        tmpCoal.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[0]).ToString();
        tmpIron.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[1]).ToString();
        tmpGold.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[2]).ToString();
        tmpDiamond.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[3]).ToString();
        tmpTorch.text = inventoryController.inventory.GetAmountItem(inventoryController.inventory.listItem[4]).ToString();
    }
}
