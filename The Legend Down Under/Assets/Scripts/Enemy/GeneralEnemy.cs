using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class GeneralEnemy : MonoBehaviour
{
    public EnemyState currentState;
    public float health;
    public FloatValue maxHealth;
    public string enemyName;
    public Vector2 homePosition;
    public int baseAttack;
    public float moveSpeed;
    // prevent enemy from attacking other enemies
    // while still staying true to the collision matrix
    public bool isEnemy = true;

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D rigidbody, float knockTime, float damage)
    {
        // only deal damage if object isn't an enemy
        if (isEnemy != true)
        {
            StartCoroutine(KnockCo(rigidbody, knockTime));
            TakeDamage(damage);
        }
        else
        {
            /* still want the enemy to produce knock back
            when colliding with an enemy to prevent
            then from overlapping */
            StartCoroutine(KnockCo(rigidbody, knockTime));
        }
    }

    private IEnumerator KnockCo(Rigidbody2D rigidbody, float knockTime)
    {
        if (rigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            rigidbody.velocity = Vector2.zero;
        }
    }
}
