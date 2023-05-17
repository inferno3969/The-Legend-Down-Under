using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveBow : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryItem bow;
    public PlayerFunctions player;

    public void OnEnable()
    {
        GiveBowToPlayer();
    }
    public void GiveBowToPlayer()
    {
        playerInventory.AddItem(bow);
    }
}