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
    [SerializeField]
    private bool firedTwoTimes;
    [SerializeField]
    private int fireCount = 0;
    public AudioSource fireSFX;
    public AudioClip chargeSound;
    public AudioClip fireSound;


    void Start()
    {
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
                if (animator.GetBool("OpenMouth") == false && animator.GetBool("CloseMouth") == false)
                {
                    OpenMouth();
                }
                else if (animator.GetBool("OpenMouth") == true)
                {
                    StartCoroutine(AttackCo());
                }
                else if (animator.GetBool("CloseMouth") == true && animator.GetBool("OpenMouth") == false && animator.GetBool("Attacking") == false)
                {
                    StartCoroutine(CloseMouthCo());
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
        canFire = true;
        fireCount = 0;
        firedTwoTimes = false;
        yield return new WaitForSeconds(2f);
        animator.SetBool("OpenMouth", true);
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        bossSFX.PlayOneShot(chargeSound);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("Attacking", true);
        if (canFire && !firedTwoTimes)
        {
            if (animator.GetBool("Attacking") == true)
            {
                if (fireCount <= 1)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    tempVector.Normalize();
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    fireSFX.PlayOneShot(fireSound);
                    current.GetComponent<Rocket>().Launch(tempVector);
                    canFire = false;
                    fireDelaySeconds = fireDelay; // Reset fire delay timer
                    fireCount++;
                }
            }
        }
        else if (fireCount == 2)
        {
            animator.SetBool("Attacking", false);
            animator.SetBool("OpenMouth", false);
            animator.SetBool("CloseMouth", true);
            canFire = false;
            firedTwoTimes = true;
        }
        else if (canFire == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
            }
        }
    }

    private IEnumerator CloseMouthCo()
    {
        yield return new WaitForSeconds(2f);
        if (animator.GetBool("WeakpointSubmerge") == true)
        {
            yield return new WaitForSeconds(2f);
            animator.SetBool("WeakpointSubmerge", false);
            yield return new WaitForSeconds(2f);
            animator.SetBool("CloseMouth", false);
        }
    }

    private IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
    }
    public IEnumerator WaitTwoSeconds()
    {
        yield return new WaitForSeconds(2f);
    }
    private IEnumerator WaitFourSeconds()
    {
        yield return new WaitForSeconds(4f);
    }
}