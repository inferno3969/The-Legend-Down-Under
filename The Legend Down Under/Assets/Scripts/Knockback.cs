using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private bool isPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Hitboxes") || other.gameObject.CompareTag("Enemy Projectile") || other.gameObject.CompareTag("Enemy Hitboxes"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Enemy"))
                {
                    // stores the player's hitboxes and rock projectile in a bool to pass through
                    // enemy knock function
                    bool rockProjectile = gameObject.CompareTag("Enemy Projectile");
                    hit.GetComponent<GeneralEnemy>().currentState = EnemyState.stagger;
                    other.GetComponent<GeneralEnemy>().Knock(hit, rockProjectile, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerFunctions>().currentState != PlayerState.stagger && other.isTrigger)
                    {
                        hit.GetComponent<PlayerFunctions>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerFunctions>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}