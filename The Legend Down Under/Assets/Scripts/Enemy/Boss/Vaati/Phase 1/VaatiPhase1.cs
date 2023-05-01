using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaatiPhase1 : BossEnemy
{
    private Rigidbody2D vaatiPhase1Rigidbody;
    Animator animator;
    [SerializeField]
    private bool shadowGuardsDefeated;
    [SerializeField]
    private GameObject[] shadowGuards;


    // Start is called before the first frame update
    void Start()
    {
        vaatiPhase1Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        triggerCollider.enabled = false;
        nonTriggerCollider.enabled = false;
        animator.SetBool("Summon", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Summon") == true)
        {
            SummonShadowGuards();
        }
    }

    private void HandDownCo()
    {
        animator.SetBool("Summon", false);
        triggerCollider.enabled = true;
        nonTriggerCollider.enabled = true;
    }
    private void SummonShadowGuards()
    {
        triggerCollider.enabled = false;
        nonTriggerCollider.enabled = false;
        if (health == 10)
        {
            shadowGuards[0].SetActive(true);
            shadowGuards[1].SetActive(true);
            if (shadowGuards[0].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[0].SetActive(false);
            }
            if (shadowGuards[1].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[1].SetActive(false);
            }
            if (shadowGuards[0].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[1].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 8)
        {
            if (health != 8)
            {
                health = 8;
            }
            shadowGuards[2].SetActive(true);
            shadowGuards[3].SetActive(true);
            if (shadowGuards[2].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[2].SetActive(false);
            }
            if (shadowGuards[3].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[3].SetActive(false);
            }
            if (shadowGuards[2].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[3].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 6)
        {
            if (health != 6)
            {
                health = 6;
            }
            shadowGuards[4].SetActive(true);
            shadowGuards[5].SetActive(true);
            if (shadowGuards[4].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[4].SetActive(false);
            }
            if (shadowGuards[5].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[5].SetActive(false);
            }
            if (shadowGuards[4].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[5].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 4)
        {
            if (health != 4)
            {
                health = 4;
            }
            shadowGuards[6].SetActive(true);
            shadowGuards[7].SetActive(true);
            if (shadowGuards[6].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[6].SetActive(false);
            }
            if (shadowGuards[7].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[7].SetActive(false);
            }
            if (shadowGuards[6].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[7].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 2)
        {
            if (health != 2)
            {
                health = 2;
            }
            shadowGuards[8].SetActive(true);
            shadowGuards[9].SetActive(true);
            if (shadowGuards[8].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[8].SetActive(false);
            }
            if (shadowGuards[9].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[9].SetActive(false);
            }
            if (shadowGuards[8].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[9].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hitboxes"))
        {
            animator.SetBool("Summon", true);
        }
    }
}