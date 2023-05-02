using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public PowerUp thisLoot;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public PowerUp LootPowerUp()
    {
        int cumalativeProbability = 0;
        int currentProbability = Random.Range(0, 100);

        for (int i = 0; i < loots.Length; i++)
        {
            cumalativeProbability += loots[i].lootChance;

            if (currentProbability <= cumalativeProbability)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }
}