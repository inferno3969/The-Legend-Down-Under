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

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D enemyRigidbody, bool player, bool rock, float knockTime, float damage)
    {
        // only deal damage if player's hitbox
        // is true when colliding
        if (player || rock)
        {
            StartCoroutine(KnockCo(enemyRigidbody, knockTime));
            TakeDamage(damage);
        }
        else
        {
            /* still want the enemy to produce knock back
            when colliding with an enemy to prevent
            then from overlapping */
            StartCoroutine(KnockCo(enemyRigidbody, knockTime));
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
