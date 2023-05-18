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
    public int numberOfArrows;
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
            if (numberOfKeys <= 0)
            {
                numberOfKeys++;
            }
            if (!myInventory.Contains(currentItem))
            {
                myInventory.Add(currentItem);
            }
        }
        else if (itemToAdd.isBossKey)
        {
            numberOfBossKeys += currentItem.numberHeld;
            if (numberOfBossKeys <= 0)
            {
                numberOfBossKeys++;
            }
            if (!myInventory.Contains(currentItem))
            {
                myInventory.Add(currentItem);
            }
        }
        else if (itemToAdd.isArrow)
        {
            numberOfArrows += currentItem.numberHeld;
            if (!myInventory.Contains(currentItem))
            {
                myInventory.Add(currentItem);
            }
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

