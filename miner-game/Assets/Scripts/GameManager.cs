using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerManager2D playerManager;
    [SerializeField] InventoryController inventoryController;

    private void Update()
    {
        if (playerManager.hitPoint <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }
}
