using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keaton : GeneralEnemy
{
    [Header("General Properties")]
    public Rigidbody2D generalEnemyRigidbody;
    public Animator animator;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    // random movement properties
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;


    void Start()
    {
        generalEnemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        latestDirectionChangeTime = 0f;
        CalcuateNewMovementVector();
    }

    // chooses random direction
    void CalcuateNewMovementVector()
    {
        int randomDirection = Random.Range(0, 4); // generates a random integer between 0 and 3

        // switch statement to determine which direction to move in
        switch (randomDirection)
        {
            case 0:
                movementDirection = Vector2.up;
                break;
            case 1:
                movementDirection = Vector2.right;
                break;
            case 2:
                movementDirection = Vector2.down;
                break;
            case 3:
                movementDirection = Vector2.left;
                break;
        }
        movementPerSecond = movementDirection * characterVelocity;
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        moveSpeed = 2;
        if (Vector3.Distance(target.position,
                            transform.position) <= chaseRadius
             && Vector3.Distance(target.position,
                               transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                moveSpeed = 6;
                Vector3 temp = Vector3.MoveTowards(transform.position,
                                                         target.position,
                                                         moveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                generalEnemyRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            // changes animation based on the random movement direction
            ChangeAnimation(movementDirection);
            ChangeState(EnemyState.walk);
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                CalcuateNewMovementVector();
            }
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime));
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

    private void SetAnimatorFloat(Vector2 setVector)
    {
        animator.SetFloat("MoveX", setVector.x);
        animator.SetFloat("MoveY", setVector.y);
    }

    // changes animation based on the direction the enemy is moving
    private void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimatorFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimatorFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimatorFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimatorFloat(Vector2.down);
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

    private IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        animator.SetBool("Attack", false);
    }
}