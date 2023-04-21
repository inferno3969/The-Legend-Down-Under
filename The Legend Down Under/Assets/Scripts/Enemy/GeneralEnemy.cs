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

    [Header("Invulnerability Frame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer enemySprite;

    public GameObject playerHitboxes;

    public GameObject enemyHitboxes;
    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            playerHitboxes.SetActive(true);
        }
    }

    public void Knock(Rigidbody2D enemyRigidbody, bool rock, float knockTime, float damage)
    {
        // only deal damage if player's hitbox or rock projectile
        // is true when colliding
        if (playerHitboxes.CompareTag("Hitboxes") || rock)
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
            if (playerHitboxes.CompareTag("Hitboxes"))
            {
                StartCoroutine(FlashCo());
            }
            yield return new WaitForSeconds(knockTime);
            rigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            rigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        playerHitboxes.SetActive(false);
        int tempFlashes;
        // turn off player trigger collider to prevent from taking damage
        triggerCollider.enabled = false;
        // prevent player from sliding when hit by an enemy
        // go through numberOfFlashes while iterating tempFlashes
        for (tempFlashes = 0; tempFlashes < numberOfFlashes; tempFlashes++)
        {
            enemySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            enemySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
        }
        // set trigger collider back on when for loop is finished
        triggerCollider.enabled = true;
        playerHitboxes.SetActive(true);
    }
}
