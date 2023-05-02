using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger
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

    [Header("Death Effects")]
    public GameObject deathEffect;
    public LootTable thisLoot;

    [Header("Invulnerability Frame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public Collider2D nonTriggerCollider;
    public SpriteRenderer enemySprite;
    public GameObject enemyHitboxes;
    public GameObject playerHitboxes;

    void OnAwake()
    {
        playerHitboxes = GameObject.FindGameObjectWithTag("Hitboxes");
    }

    void Start()
    {
        playerHitboxes = GetComponent<PlayerFunctions>().playerSwordHitboxes;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            MakeLoot();
            this.gameObject.SetActive(false);
            playerHitboxes.SetActive(true);
        }
    }

    public void Knock(Rigidbody2D enemyRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(enemyRigidbody, knockTime));
        TakeDamage(damage);
    }

    public void KnockNoDamage(Rigidbody2D enemyRigidbody, float knockTime)
    {
        StartCoroutine(KnockCoNoDamage(enemyRigidbody, knockTime));
    }

    private IEnumerator KnockCoNoDamage(Rigidbody2D rigidbody, float knockTime)
    {
        if (rigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidbody.velocity = Vector2.zero;
            currentState = EnemyState.Idle;
            rigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator KnockCo(Rigidbody2D rigidbody, float knockTime)
    {
        if (rigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            rigidbody.velocity = Vector2.zero;
            currentState = EnemyState.Idle;
            rigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        playerHitboxes.SetActive(false);
        int tempFlashes;
        // turn off player trigger collider to prevent from taking damage
        triggerCollider.enabled = false;
        nonTriggerCollider.enabled = false;
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
        nonTriggerCollider.enabled = true;
        playerHitboxes.SetActive(true);
    }

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            PowerUp currentLoot = thisLoot.LootPowerUp();
            if (currentLoot != null)
            {
                Instantiate(currentLoot.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}
