using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZeldaDirection
{
    Up,
    Down,
    Left,
    Right
}

public class Zelda : GeneralNPC
{
    private Rigidbody2D zeldaRigidbody;
    private Animator animator;
    public Transform target;
    public float moveSpeed;
    [SerializeField]
    private ZeldaDirection direction;
    [SerializeField] float targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        zeldaRigidbody = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // Check for collisions in the direction of movement
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, moveSpeed * Time.deltaTime);
        if (hit.collider != null)
        {
            if (Vector2.Distance(transform.position, target.position) > 1.4)
            {
                animator.SetBool("Moving", true);
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnimation(target.position - transform.position);
            }
            else if (Vector2.Distance(transform.position, target.position) < 1.4 && Vector2.Distance(transform.position, target.position) > 1.3)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnimation(target.position - transform.position);
            }
            else
            {
                // Move towards the target
                animator.SetBool("Moving", false);
            }
        }
    }

    public void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimatorFloat(Vector2.right);
                this.direction = ZeldaDirection.Right;
            }
            else if (direction.x < 0)
            {
                SetAnimatorFloat(Vector2.left);
                this.direction = ZeldaDirection.Left;
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimatorFloat(Vector2.up);
                this.direction = ZeldaDirection.Up;
            }
            else if (direction.y < 0)
            {
                SetAnimatorFloat(Vector2.down);
                this.direction = ZeldaDirection.Down;
            }
        }
    }

    private void SetAnimatorFloat(Vector2 setVector)
    {
        animator.SetFloat("MoveX", setVector.x);
        animator.SetFloat("MoveY", setVector.y);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            zeldaRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        zeldaRigidbody.constraints = RigidbodyConstraints2D.None;
        zeldaRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
