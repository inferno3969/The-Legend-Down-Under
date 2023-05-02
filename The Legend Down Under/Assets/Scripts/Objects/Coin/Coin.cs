using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        powerUpSignal.RaiseSignal();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coins += 1;
            powerUpSignal.RaiseSignal();
            Destroy(this.gameObject);
        }
    }
}
