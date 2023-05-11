using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public InventoryItem currentItem;
    public List<InventoryItem> myInventory = new List<InventoryItem>();
    public int numberOfKeys;
    public int numberOfBossKeys;
    public int coins;

    public bool CheckForItem(InventoryItem item)
    {
        if (myInventory.Contains(item))
        {
            return true;
        }
        return false;
    }

    public void AddItem(InventoryItem itemToAdd)
    {
        currentItem = itemToAdd;
        // is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys += currentItem.numberHeld;
            currentItem.numberHeld = numberOfKeys;
            if (!myInventory.Contains(currentItem))
            {
                myInventory.Add(currentItem);
            }
        }
        else if (itemToAdd.isBossKey)
        {
            numberOfBossKeys++;
            myInventory.Add(currentItem);
        }
        else
        {
            if (!myInventory.Contains(currentItem))
            {
                myInventory.Add(currentItem);
            }
        }
    }
}

