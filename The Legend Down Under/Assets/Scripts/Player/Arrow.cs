using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D arrowRigidbody;
    public float lifetime;
    private float lifetimeCounter;
    public Animator weakpoint;

    // Start is called before the first frame update
    void Awake()
    {
        weakpoint = GameObject.FindGameObjectWithTag("Boss Weakpoint").GetComponent<Animator>();
    }

    void Start()
    {
        lifetimeCounter = lifetime;
    }

    private void Update()
    {
        lifetimeCounter -= Time.deltaTime;
        if (lifetimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        arrowRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")
        || other.gameObject.CompareTag("Boss Weakpoint"))
        {
            Destroy(this.gameObject);
            weakpoint.GetComponent<Animator>().SetBool("WeakpointSubmerge", true);
        }
    }
}