using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// state machine for player
public enum PlayerState
{
    idle,
    walk,
    attack,
    interact
}

public class PlayerFunctions : MonoBehaviour
{
    // public variables (seen in Unity inspector)
    public PlayerState currentState;
    public float speed;

    // [HideInInspector] field hides the public variable in Unity inspector
    [HideInInspector]
    public Animator animator;

    // private variables that can't be seen in Unity
    private Rigidbody2D playerRigidBody;
    private Vector3 change;

    // Start is called before the first frame update
    void Start()
    {
        // set current state if player to idle
        currentState = PlayerState.idle;

        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // changes sprite based on Input 
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        // initiate attack coroutine when attack Input is pressed and player current state doesn't
        // equal attack and walk and equals idle
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState == PlayerState.idle && currentState != PlayerState.walk)
        {
            StartCoroutine(AttackCo());
        }
        // updates animation and moves when player current state equals walk or equals idle
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            // this if else block prevents player from moving diagonally
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

    // moves position of player
    void MoveCharacter()
    {
        playerRigidBody.MovePosition(transform.position + speed * Time.deltaTime * change.normalized);
    }

    void UpdateAnimationAndMove()
    {
        // moves player and updates walking animations based on change in X and Y axis
        if (change != Vector3.zero)
        {
            transform.Translate(new Vector3(change.x, change.y));
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        // stops walking animations and player goes to idle state when not moving
        else
        {
            animator.SetBool("Moving", false);
            currentState = PlayerState.idle;
        }
    }

    // coroutine for attack to play attack animation and reset to idle after animation
    // and short delay
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