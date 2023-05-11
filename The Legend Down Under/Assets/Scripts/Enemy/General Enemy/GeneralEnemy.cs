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
    public SignalSender roomSignal;

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
    public GameObject treasureChest;

    void Awake()
    {
        homePosition = transform.position;
        playerHitboxes = GameObject.FindGameObjectWithTag("Hitboxes");
        health = maxHealth.initialValue;
    }

    private void OnEnable()
    {
        transform.position = homePosition;
        health = maxHealth.initialValue;
        currentState = EnemyState.Idle;
        triggerCollider.enabled = true;
        nonTriggerCollider.enabled = true;
        enemySprite.color = regularColor;
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
            // if we don't want the enemy to send out a signal,
            // we leave the roomSignal raise optional 
            if (roomSignal != null)
            {
                roomSignal.RaiseSignal();
            }
            if (treasureChest != null)
            {
                treasureChest.SetActive(true);
            }
            this.gameObject.SetActive(false);
            playerHitboxes.SetActive(true);
        }
    }

    public void Knock(Rigidbody2D enemyRigidbody, float knockTime, float damage)
    {
        if (enemyRigidbody != null)
        {
            StartCoroutine(KnockCo(enemyRigidbody, knockTime, damage));
        }
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

    private IEnumerator KnockCo(Rigidbody2D rigidbody, float knockTime, float damage)
    {
        if (rigidbody != null)
        {
            StartCoroutine(FlashCo(damage));
            yield return new WaitForSeconds(knockTime);
            rigidbody.velocity = Vector2.zero;
            currentState = EnemyState.Idle;
            rigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo(float damage)
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
        damage = 2;
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