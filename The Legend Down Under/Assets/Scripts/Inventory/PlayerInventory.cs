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
    public bool isKey;
    public bool isBossKey;

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
        // is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
            if (currentItem != null)
            {
                currentItem.numberHeld = numberOfKeys;
                if (!myInventory.Contains(itemToAdd))
                {
                    myInventory.Add(itemToAdd);
                }
            }
        }
        else if (itemToAdd.isBossKey)
        {
            numberOfBossKeys++;
            myInventory.Add(itemToAdd); 
        }
        else
        {
            if (!myInventory.Contains(itemToAdd))
            {
                myInventory.Add(itemToAdd);
            }
        }
    }
}

