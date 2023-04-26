using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gleerok : BossEnemy
{
    private Rigidbody2D gleerokRigidbody;
    Animator animator;
    public Transform target;
    public float chaseRadius;
    public float AttackRadius;
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire;
    private bool firedThreeTimes;
    [SerializeField]
    private int fireCount = 0;


    void Start()
    {
        canFire = true;
        firedThreeTimes = false;
        currentState = BossEnemyState.Idle;
        animator = GetComponent<Animator>();
        gleerokRigidbody = GetComponent<Rigidbody2D>();
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
                               transform.position) > AttackRadius)
        {
            if (currentState == BossEnemyState.Idle || currentState != BossEnemyState.Walk
                && currentState != BossEnemyState.Stagger)
            {

                if (animator.GetBool("OpenMouth") != false)
                {
                    OpenMouth();
                    if (canFire && !firedThreeTimes)
                    {
                        if (animator.GetBool("OpenMouth") == true && animator.GetBool("CloseMouth") == false)
                        {
                            for (fireCount = 0; fireCount < 4; fireCount++)
                            {
                                Vector3 tempVector = target.transform.position - transform.position;
                                tempVector.Normalize();
                                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                                StartCoroutine(Wait());
                                current.GetComponent<Rocket>().Launch(tempVector);
                                fireDelaySeconds = fireDelay;
                                fireCount++;
                            }
                        }
                        canFire = false;
                        firedThreeTimes = true;
                        animator.SetBool("OpenMouth", true);
                        animator.SetBool("CloseMouth", false);
                    }
                    // Vector3 tempVector = target.transform.position - transform.position;
                    // tempVector.Normalize();
                    // GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    // // int random = Random.Range(1, 3);
                    // // current.transform.localScale = new Vector3(random, random, 0);
                    // current.GetComponent<Rocket>().Launch(tempVector);
                    // canFire = false;
                    // fireDelaySeconds = fireDelay; // Reset fire delay timer

                }
                else if (canFire)
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

    private void OpenMouth()
    {
        StartCoroutine(OpenMouthCo());
    }

    private IEnumerator OpenMouthCo()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("OpenMouth", false);
        yield return new WaitForSeconds(2f);
        animator.SetBool("OpenMouth", true);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
