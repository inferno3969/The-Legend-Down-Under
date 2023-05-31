using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    public Animator animator;
    public AudioClip explosionSound;

    private void Awake()
    {
        animator.SetBool("Explode", false);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        projectileSFX.PlayOneShot(explosionSound);
        animator.SetBool("Explode", true);
        yield return new WaitForSeconds(0.42f);
        Destroy(this.gameObject);
    }
}
