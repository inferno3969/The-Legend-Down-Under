using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolKeaton : PatrolEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        generalEnemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPatrolTarget = patrolPoints[currentPatrolPoint];
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                            transform.position) <= chaseRadius
             && Vector3.Distance(target.position,
                               transform.position) > AttackRadius)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk
                && currentState != EnemyState.Stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                                                         target.position,
                                                         moveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                generalEnemyRigidbody.MovePosition(temp);
                ChangeState(EnemyState.Walk);
            }
        }
        else if (Vector3.Distance(target.position,
            transform.position) <= chaseRadius
            && Vector3.Distance(target.position,
            transform.position) <= AttackRadius)
        {
            if (currentState == EnemyState.Walk
                && currentState != EnemyState.Stagger && generalEnemyRigidbody != null)
            {
                StartCoroutine(AttackCo());
            }
        }
        else if (Vector3.Distance(target.position,
                    transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                patrolPoints[currentPatrolPoint].position,
                moveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                generalEnemyRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private IEnumerator AttackCo()
    {
        currentState = EnemyState.Attack;
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.Walk;
        animator.SetBool("Attacking", false);
    }
}
