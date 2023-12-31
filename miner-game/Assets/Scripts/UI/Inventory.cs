using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] internal InventoryController inventoryController;

    [SerializeField] Item coalMineralItem;
    [SerializeField] Item ironMineralItem;
    [SerializeField] Item goldMineralItem;
    [SerializeField] Item diamondMineralItem;
    [SerializeField] Item torchItem;

    internal Item[] listItem;
    internal Dictionary<Item, int> itemInventory = new Dictionary<Item, int>();

    private void Awake()
    {
        listItem = new Item[] {coalMineralItem, ironMineralItem, goldMineralItem, diamondMineralItem, torchItem };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddItem(coalMineralItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddItem(ironMineralItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddItem(goldMineralItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddItem(diamondMineralItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventoryController.player.GetComponent<PlayerManager2D>().GetHit(1);
        }
    }

    public void GetHit(int dmg)
    {
        inventoryController.player.GetComponent<PlayerManager2D>().hitPoint-=dmg;
        inventoryController.UpdateDisplay();
    }

    public void AddItem(Item item)
    {
        if (itemInventory.ContainsKey(item))
        {
            itemInventory[item]++;
        }
        else
        {
            itemInventory.Add(item, 1);
        }
        inventoryController.player.GetComponent<PlayerManager2D>().score += item.scoreGiven;
        inventoryController.UpdateDisplay();
    }

    public void AddItem(Item item, int amount)
    {
        for(int i = 0; i < amount; i++) AddItem(item);
    }

    public void RemoveItem(Item item)
    {
        if (itemInventory.ContainsKey(item))
        {
            itemInventory[item]--;

            if (itemInventory[item] <= 0)
            {
                itemInventory.Remove(item);
            }
        }
        inventoryController.UpdateDisplay();
    }

    public void RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < amount; i++) RemoveItem(item);
    }

    public bool ContainAmountItem(Item item, int amount)
    {
        return itemInventory[item] >= amount;
    }

    public int GetAmountItem(Item item)
    {
        if (!itemInventory.ContainsKey(item))return 0;
        return itemInventory[item];
    }

}
