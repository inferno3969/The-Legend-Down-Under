using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOneBarrier : Barrier
{
    public BoolValue swordAndShieldAcquired;

    // Update is called once per frame
    void Update()
    {
        if (swordAndShieldAcquired.RuntimeValue)
        {
            barriers.SetActive(false);
        }
    }
}
