using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBreak : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryItem shield;
    public PlayerFunctions player;

    public void OnEnable()
    {
        BreakShield();
    }

    public void BreakShield()
    {
        playerInventory.myInventory.Remove(shield);
    }
}