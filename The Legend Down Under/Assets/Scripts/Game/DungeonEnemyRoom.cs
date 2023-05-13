using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;
    [SerializeField] private int defeatedEnemies = 0;
    public Collider2D roomTrigger;

    public void EnemyDefeated()
    {
        defeatedEnemies++;
        Debug.Log(defeatedEnemies);
        CheckEnemies();
    }

    public void CheckEnemies()
    {
        if (defeatedEnemies == enemies.Length)
        {
            OpenDoors();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomTrigger.enabled = false;
            if (defeatedEnemies == 0)
            {
                //Activate all enemies and pots
                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i], true);
                }
                for (int i = 0; i < pots.Length; i++)
                {
                    ChangeActivation(pots[i], true);
                }
                CloseDoors();
            }
        }
    }

    // empty override to prevent the base class from doing anything
    public override void OnTriggerExit2D(Collider2D other)
    {
        defeatedEnemies = 0;
    }

    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}