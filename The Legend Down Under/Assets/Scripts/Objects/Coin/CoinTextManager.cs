using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    private void Start()
    {
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        coinDisplay.text = "" + playerInventory.coins;
    }
}
