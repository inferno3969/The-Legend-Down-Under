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
        // otherwise, check the distance between the player and the enemy
        else 
        {
            CheckDistance();
        }
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
        transform.position) <= chaseRadius
           && Vector3.Distance(target.position,
                               transform.position) > AttackRadius)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position,
    target.position, moveSpeed * Time.deltaTime);
            ChangeAnimation(temp - transform.position);
            if (currentState == EnemyState.Idle || currentState != EnemyState.Walk
                && currentState != EnemyState.Stagger)
            {
                if (canFire)
                {
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