using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBow : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryItem bow;
    public PlayerFunctions player;
    public InventoryItem arrow;

    public void OnEnable()
    {
        BreakBowAndRemoveArrows();
    }

    public void BreakBowAndRemoveArrows()
    {
        // remove bow and all arrows from player inventory
        playerInventory.myInventory.Remove(bow);
        playerInventory.myInventory.Remove(arrow);
        playerInventory.numberOfArrows = 0;
        arrow.numberHeld = 0;
    }
}
