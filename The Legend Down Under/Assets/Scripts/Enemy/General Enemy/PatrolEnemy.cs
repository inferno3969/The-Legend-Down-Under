using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : GeneralEnemy
{
    public Rigidbody2D generalEnemyRigidbody;
    [Header("Target Variables")]
    public Transform target;
    public float chaseRadius;
    public float AttackRadius;

    [Header("Animator")]
    public Animator animator;

    [Header("Patrol Properties")]
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    public Transform currentPatrolTarget;
    public float roundingDistance;

    void Start()
    {
        generalEnemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPatrolTarget = patrolPoints[currentPatrolPoint];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
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
                animator.SetBool("WakeUp", true);
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

    public void SetAnimationFloat(Vector2 setVector)
    {
        animator.SetFloat("MoveX", setVector.x);
        animator.SetFloat("MoveY", setVector.y);
    }

    public void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimationFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimationFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimationFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimationFloat(Vector2.down);
            }
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void ChangeGoal()
    {
        if (currentPatrolPoint == patrolPoints.Length - 1)
        {
            currentPatrolPoint = 0;
            currentPatrolTarget = patrolPoints[0];
        }
        else
        {
            currentPatrolPoint++;
            currentPatrolTarget = patrolPoints[currentPatrolPoint];
        }
    }
}