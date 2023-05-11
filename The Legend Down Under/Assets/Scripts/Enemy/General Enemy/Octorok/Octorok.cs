using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class Octorok : GeneralEnemy
{
    public Rigidbody2D generalEnemyRigidbody;
    public Transform target;
    public float chaseRadius;
    public float AttackRadius;
    public Animator animator;

    protected Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.Idle;
        if (generalEnemyRigidbody != null)
        {
            generalEnemyRigidbody = GetComponent<Rigidbody2D>();
        }
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void OnEnable()
    {
        if (playerHitboxes == null)
        {
            playerHitboxes = GetComponent<PlayerFunctions>().playerSwordHitboxes;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > AttackRadius)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk && currentState != EnemyState.Stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                generalEnemyRigidbody.MovePosition(temp);
                ChangeState(EnemyState.Walk);
                animator.SetBool("InRange", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animator.SetBool("InRange", false);
        }
    }

    private void SetAnimatorFloat(Vector2 setVector)
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
                SetAnimatorFloat(Vector2.right);
                this.direction = Direction.Right;
            }
            else if (direction.x < 0)
            {
                SetAnimatorFloat(Vector2.left);
                this.direction = Direction.Left;
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimatorFloat(Vector2.up);
                this.direction = Direction.Up;
            }
            else if (direction.y < 0)
            {
                SetAnimatorFloat(Vector2.down);
                this.direction = Direction.Down;
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
}