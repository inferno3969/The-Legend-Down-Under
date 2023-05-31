using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : Projectile
{
    public AudioClip rockHitSound;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            projectileSFX.PlayOneShot(rockHitSound);
            Destroy(this.gameObject);
        }
    }
}