using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : GeneralEnemy
{


    public Rigidbody2D generalEnemyRigidbody;
    [Header("Target Variables")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    [Header("Animator")]
    public Animator animator;


    // Use this for initialization
    void Start()
    {
        currentState = EnemyState.idle;
        generalEnemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
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
                               transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                                                         target.position,
                                                         moveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                generalEnemyRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("WakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position,
                            transform.position) > chaseRadius)
        {
            animator.SetBool("WakeUp", false);
        }
    }

    private void SetAnimationFloat(Vector2 setVector)
    {
        animator.SetFloat("MoveX", setVector.x);
        animator.SetFloat("MoveY", setVector.y);
    }

    private void ChangeAnimation(Vector2 direction)
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

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}