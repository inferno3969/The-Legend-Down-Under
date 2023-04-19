using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOcotorok : Octorok
{
    public GameObject projectile;
    public PlayerFunctions player;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        if (!canFire)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = fireDelay;
            }
        }
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
        transform.position) <= chaseRadius
           && Vector3.Distance(target.position,
                               transform.position) > attackRadius)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position,
    target.position, moveSpeed * Time.deltaTime);
            ChangeAnimation(temp - transform.position);
            if (currentState == EnemyState.idle || currentState != EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    if (Mathf.Abs(tempVector.x) > Mathf.Abs(tempVector.y))
                    {
                        tempVector.y = 0;
                    }
                    else
                    {
                        tempVector.x = 0;
                    }
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                }
            }
        }
        else if (Vector3.Distance(target.position,
                           transform.position) > chaseRadius)
        {
            animator.SetBool("InRange", false);
        }
    }
}