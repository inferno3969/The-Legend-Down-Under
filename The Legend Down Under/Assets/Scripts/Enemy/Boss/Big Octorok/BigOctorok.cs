using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOctorok : BossEnemy
{
    private Rigidbody2D bigOctorokRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire;

    void Start()
    {
        canFire = true;
        currentState = BossEnemyState.idle;
        bigOctorokRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        fireDelaySeconds = fireDelay; // Initialize fire delay variable
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
                               transform.position) > attackRadius)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position,
    target.position, moveSpeed * Time.deltaTime);
            if (currentState == BossEnemyState.idle || currentState != BossEnemyState.walk
                && currentState != BossEnemyState.stagger)
            {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    tempVector.Normalize();
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                    fireDelaySeconds = fireDelay; // Reset fire delay timer
                }
                else
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
}