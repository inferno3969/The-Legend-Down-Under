using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

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
    public GameObject teleport;
    public GameObject heartContainer;
    public bool newAudio;
    public IntroloopAudio clipToChangeTo;

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

    public GameObject transition;
    public GameObject transition2;

    public bool isOctorok;
    public bool isGleerok;

    [Header("SFX")]
    public AudioSource bossSFX;
    public AudioClip bossHitSound;
    public AudioClip bossDeathSound;

    void Awake()
    {
        playerHitboxes = GameObject.FindGameObjectWithTag("Hitboxes");
    }

    void Start()
    {
        playerHitboxes = GetComponent<PlayerFunctions>().playerSwordHitboxes;
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        playerHitboxes.SetActive(false);
        health -= damage;
        if (health > 0 && bossHitSound != null)
        {
            bossSFX.PlayOneShot(bossHitSound);
        }
        if (health <= 0)
        {
            if (bossDeathSound != null)
            {
                bossSFX.PlayOneShot(bossDeathSound);
            }
            if (newAudio)
            {
                BGSoundScript.Instance.gameObject.GetComponent<IntroloopPlayer>().Stop();
                BGSoundScript.Instance.gameObject.GetComponent<IntroloopPlayer>().Play(clipToChangeTo);
            }
            if (teleport != null)
            {
                teleport.SetActive(true);
            }
            if (heartContainer != null)
            {
                heartContainer.SetActive(true);
            }
            if (isOctorok)
            {
                transition.SetActive(true);
            }
            if (isGleerok)
            {
                transition2.SetActive(true);
            }
            this.gameObject.SetActive(false);
            playerHitboxes.SetActive(true);
        }
    }

    public void Knock(Rigidbody2D enemyRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(enemyRigidbody, knockTime));
        TakeDamage(damage);
        playerHitboxes.SetActive(true);
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
    }
}
