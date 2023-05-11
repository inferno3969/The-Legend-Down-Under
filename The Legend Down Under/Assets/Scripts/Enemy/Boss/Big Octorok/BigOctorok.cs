using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BigOctorok : BossEnemy
{
    private Rigidbody2D bigOctorokRigidbody;
    public Transform target;
    public float chaseRadius;
    public float AttackRadius;
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire;

    private bool octorokMinionDefeated = true;

    [SerializeField]
    private GameObject[] octoroks;

    void Start()
    {
        canFire = true;
        currentState = BossEnemyState.Idle;
        bigOctorokRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        fireDelaySeconds = fireDelay; // Initialize fire delay variable
    }

    private void Update()
    {
        CheckDistance();
        SpawnOctoroks();
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
                if (canFire && octorokMinionDefeated == true)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    tempVector.Normalize();
                    GameObject current = Instantiate(projectile, transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
                    // int random = Random.Range(1, 3);
                    // current.transform.localScale = new Vector3(random, random, 0);
                    current.GetComponent<BigRockProjectile>().Launch(tempVector, 0);
                    canFire = false;
                    fireDelaySeconds = fireDelay; // Reset fire delay timer
                }
                else if (canFire == false && octorokMinionDefeated == true)
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

    private void SpawnOctoroks()
    {
        // spawn octoroks
        if (health == 6)
        {
            octoroks[0].SetActive(true);
            octoroks[0].GetComponent<Octorok>().playerHitboxes = GameObject.FindGameObjectWithTag("Hitboxes");
            octorokMinionDefeated = false;
            if (octoroks[0].GetComponent<Octorok>().health <= 0)
            {
                octoroks[0].SetActive(false);
                octorokMinionDefeated = true;
            }
        }
        if (health == 4)
        {
            octoroks[1].SetActive(true);
            octoroks[1].GetComponent<Octorok>().playerHitboxes = GameObject.FindGameObjectWithTag("Hitboxes");
            octorokMinionDefeated = false;
            if (octoroks[1].GetComponent<Octorok>().health <= 0)
            {
                octoroks[1].SetActive(false);
                octorokMinionDefeated = true;
            }
        }
        if (health == 2)
        {
            octoroks[2].SetActive(true);
            octoroks[2].GetComponent<Octorok>().playerHitboxes = GameObject.FindGameObjectWithTag("Hitboxes");
            octorokMinionDefeated = false;
            if (octoroks[2].GetComponent<Octorok>().health <= 0)
            {
                octoroks[2].SetActive(false);
                octorokMinionDefeated = true;
            }
        }
    }
}