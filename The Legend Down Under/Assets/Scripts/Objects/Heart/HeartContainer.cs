using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : PowerUp
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            heartContainers.RuntimeValue += 1;
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            playerHealth.initialValue = playerHealth.RuntimeValue;
            powerUpSignal.RaiseSignal();
            Destroy(this.gameObject);
        }
    }
}