using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stat_UI : MonoBehaviour
{
    [SerializeField] GameObject player;

    [Header("HitPoint")]
    [SerializeField] Image hitPointImage;
    [SerializeField] Sprite[] hitPointSpriteTab;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI tmpScore;

    [Header("Inventory")]
    [SerializeField] Image pickaxe;
    [SerializeField] Sprite[] pickaxeSpriteTab;
    [SerializeField] TextMeshProUGUI tmpTorch;
    [SerializeField] TextMeshProUGUI tmpCoal;
    [SerializeField] TextMeshProUGUI tmpIron;
    [SerializeField] TextMeshProUGUI tmpGold;
    [SerializeField] TextMeshProUGUI tmpDiamond;

    void UpdateHitPointDisplay()
    {
        //hitPointImage.sprite = hitPointSpriteTab[player.GetComponent<PlayerStat>().hitPoints];
    }

    void UpdateScoreDisplay()
    {
        //tmpScore.text = player.GetComponent<PlayerStat>().score;
    }

    void UpdateInventoryDisplay()
    {
        //pickaxe.sprite = pickaxeSpriteTab[player.GetComponent<PlayerStat>().pickaxeLevel];
        //tmpTorch.text = player.GetComponent<PlayerStat>().torchCount;
        //tmpCoal.text = player.GetComponent<PlayerStat>().coalCount;
        //tmpIron.text = player.GetComponent<PlayerStat>().ironCount;
        //tmpGold.text = player.GetComponent<PlayerStat>().goldCount;
        //tmpDiamond.text = player.GetComponent<PlayerStat>().diamondCount;
    }
}
