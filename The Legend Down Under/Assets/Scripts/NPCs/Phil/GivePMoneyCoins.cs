using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePMoneyCoins : MonoBehaviour
{
    public SignalSender coinSignal;
    public PlayerInventory playerInventory;

    public void OnEnable()
    {
        playerInventory.coins += 10;
        coinSignal.RaiseSignal();
    }
}
