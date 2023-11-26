using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<Item, int> itemInventory = new Dictionary<Item, int>();

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
    }

    public void RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < amount; i++) RemoveItem(item);
    }

    public bool ContainAmountMineral(Item item, int amount)
    {
        return itemInventory[item] >= amount;
    }

}
