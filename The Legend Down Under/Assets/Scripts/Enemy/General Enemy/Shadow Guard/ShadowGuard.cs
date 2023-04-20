using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGuard : PatrolEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        generalEnemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPatrolTarget = patrolPoints[currentPatrolPoint];
        animator.SetBool("Idle", true);
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                            transform.position) <= chaseRadius
             && Vector3.Distance(target.position,
                               transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                                                         target.position,
                                                         moveSpeed * Time.deltaTime);
                animator.SetBool("Idle", false);                                                        
                ChangeAnimation(temp - transform.position);
                generalEnemyRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.position,
                            transform.position) > chaseRadius && animator.GetBool("Attacking") == false && animator.GetBool("Idle") == false)
        {
            moveSpeed = 6;
            Vector3 temp = Vector3.MoveTowards(transform.position,
            patrolPoints[currentPatrolPoint].position,
            moveSpeed * Time.deltaTime);
            ChangeAnimation(temp - transform.position);
            generalEnemyRigidbody.MovePosition(temp);
            // set move speed to 2 when enemy has reached its patrol point
            if (transform.position == patrolPoints[0].position)
            {
                SetAnimationFloat(Vector2.down);
                animator.SetBool("Idle", true);
                moveSpeed = 2;
            }
            
        }
        else if (Vector3.Distance(target.position,
                    transform.position) <= chaseRadius
                    && Vector3.Distance(target.position,
                    transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

        private IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        animator.SetBool("Attacking", false);
    }
}
