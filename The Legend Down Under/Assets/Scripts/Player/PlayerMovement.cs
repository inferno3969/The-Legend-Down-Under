using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D playerRigidBody;
    private Vector3 change;
    [HideInInspector]
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.idle;
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState == PlayerState.idle && currentState != PlayerState.walk)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            if (Mathf.Abs(change.x) > Mathf.Abs(change.y))
            {
                change.y = 0;
            }
            else
            {
                change.x = 0;
            }
            UpdateAnimationAndMove();
        }
    }

    void MoveCharacter()
    {
        playerRigidBody.MovePosition(transform.position + speed * Time.deltaTime * change.normalized);
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            transform.Translate(new Vector3(change.x, change.y));
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
            currentState = PlayerState.idle;
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.idle;
    }
}
