using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // enemy projectile to be reflected when entering shield trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // if projectile is enemy projectile
        // reflect projectile
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            // get projectile's rigidbody
            Rigidbody2D projectileRigidBody = other.GetComponent<Rigidbody2D>();
            projectileRigidBody.excludeLayers = 0;
            // get projectile's direction
            Vector2 projectileDirection = projectileRigidBody.velocity;
            // reflect projectile
            projectileRigidBody.velocity = -projectileDirection;
        }
    }
}