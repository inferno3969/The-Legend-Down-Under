using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossEnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger
}

public class BossEnemy : MonoBehaviour
{
    [Header("General Properties")]
    public BossEnemyState currentState;
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
    public Collider2D nonTriggerCollider;
    public SpriteRenderer enemySprite;
    public GameObject enemyHitboxes;
    public GameObject playerHitboxes;

    void Awake()
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
            this.gameObject.SetActive(false);
            playerHitboxes.SetActive(true);
        }
    }

    public void Knock(Rigidbody2D enemyRigidbody, float knockTime, float damage)
    {
        {
            StartCoroutine(KnockCo(enemyRigidbody, knockTime));
            TakeDamage(damage);
        }
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
            currentState = BossEnemyState.Idle;
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
            currentState = BossEnemyState.Idle;
            rigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        playerHitboxes.SetActive(false);
        int tempFlashes;
        // turn off player trigger collider to prevent from taking damage
        triggerCollider.enabled = false;
        if (nonTriggerCollider != null)
        {
            nonTriggerCollider.enabled = false;
        }
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
        if (nonTriggerCollider != null)
        {
            nonTriggerCollider.enabled = true;
        }
        playerHitboxes.SetActive(true);
    }
}
