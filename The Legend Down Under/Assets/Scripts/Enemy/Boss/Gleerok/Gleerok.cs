using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gleerok : BossEnemy
{
    private Rigidbody2D gleerokRigidbody;
    Animator animator;
    public Transform target;
    public float chaseRadius;
    public float AttackRadius;
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire;
    private bool firedThreeTimes = false;


    void Start()
    {
        canFire = true;
        currentState = BossEnemyState.Idle;
        animator = GetComponent<Animator>();
        gleerokRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        fireDelaySeconds = fireDelay; // Initialize fire delay variable
        animator.SetBool("MouthOpened", false);
    }

    private void Update()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        if (Vector3.Distance(target.position,
        transform.position) <= chaseRadius
           && Vector3.Distance(target.position,
                               transform.position) > AttackRadius)
        {
            if (currentState == BossEnemyState.Idle || currentState != BossEnemyState.Walk
                && currentState != BossEnemyState.Stagger)
            {
                if (canFire && firedThreeTimes == false)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    tempVector.Normalize();
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    // int random = Random.Range(1, 3);
                    // current.transform.localScale = new Vector3(random, random, 0);
                    current.GetComponent<Rocket>().Launch(tempVector);
                    canFire = false;
                    fireDelaySeconds = fireDelay; // Reset fire delay timer

                }
                else if (canFire == false && firedThreeTimes == true)
                {
                    fireDelaySeconds -= Time.deltaTime;
                    if (fireDelaySeconds <= 0)
                    {
                        canFire = true;
                    }
                }
            }
        }
    }

    private void Emerge()
    {
        animator.SetBool("WeakpointHit", false);
    }

    private void GleerokSubmerge()
    {
        StartCoroutine(WaitToSubmerge());
    }

    private IEnumerator WaitToSubmerge()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("MouthClosed", true);
    }

    private IEnumerator WaitToEmerge()
    {
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator OpenMouth()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("MouthOpened", true);
    }
}
