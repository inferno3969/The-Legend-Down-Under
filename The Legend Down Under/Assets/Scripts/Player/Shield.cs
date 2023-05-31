using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public AudioSource deflectSound;
    // enemy projectile to be reflected when entering shield trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // if projectile is enemy projectile,
        // reflect projectile
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            // get projectile's rigidbody
            Rigidbody2D projectileRigidBody = other.GetComponent<Rigidbody2D>();
            /* allows projectile to collide with Enemy layer
            since initially excludes the Enemy layer 
            to prevent from hurting the enemy when launched */
            projectileRigidBody.excludeLayers = 0;
            // get projectile's direction
            Vector2 projectileDirection = projectileRigidBody.velocity;
            // play deflect sound
            deflectSound.Play();
            // reflect projectile
            projectileRigidBody.velocity = -projectileDirection;
        }
    }
}