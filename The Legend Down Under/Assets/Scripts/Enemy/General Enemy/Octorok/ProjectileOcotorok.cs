using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOcotorok : Octorok
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire;

    void Start()
    {
        canFire = true;
        fireDelaySeconds = fireDelay;
        animator = GetComponent<Animator>();
        generalEnemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // will count down if projectile has been fired
        if (canFire == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = fireDelay;
            }
        }
        CheckDistance();
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
        transform.position) <= chaseRadius)
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
        else if (Vector3.Distance(target.position,
            transform.position) > chaseRadius && Vector3.Distance(target.position, transform.position) <= AttackRadius)
        {
            animator.SetBool("InRange", true);
            if (canFire)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                GameObject current = Instantiate(projectile, transform.position + GetProjectileOffset(), Quaternion.identity);
                Vector3 tempVector = target.transform.position - transform.position;
                tempVector.Normalize();
                if (Mathf.Abs(tempVector.x) > Mathf.Abs(tempVector.y))
                {
                    tempVector.y = 0;
                }
                else
                {
                    tempVector.x = 0;
                }
                current.GetComponent<Projectile>().Launch(tempVector);
                canFire = false;
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
        else if (Vector3.Distance(target.position,
                           transform.position) > chaseRadius)
        {
            animator.SetBool("InRange", false);
        }
    }

    private Vector3 GetProjectileOffset()
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector3(0, 0.5f, 0);
            case Direction.Down:
                return new Vector3(0, -0.5f, 0);
            case Direction.Left:
                return new Vector3(-0.5f, 0, 0);
            case Direction.Right:
                return new Vector3(0.5f, 0, 0);
            default:
                return Vector3.zero;
        }
    }
}