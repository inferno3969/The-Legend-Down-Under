using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// state machine for player
public enum PlayerState
{
    idle,
    walk,
    attack,
    stagger,
    interact,
    shield
}

public class PlayerFunctions : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;

    [Header("Player Health")]
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    // public variables (seen in Unity inspector)

    // [HideInInspector] field hides the public variable in Unity inspector
    [HideInInspector]
    public Animator animator;

    // private variables that can't be seen in Unity
    private Rigidbody2D playerRigidBody;
    private Vector3 change;

    [Header("Player Inventory")]
    public PlayerInventory playerInventory;

    [Header("Weapons")]
    public InventoryItem soldierSword;

    // Start is called before the first frame update
    void Start()
    {
        // set current state if player to idle
        currentState = PlayerState.walk;
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // changes sprite based on Input 
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        Update();
    }

    void Update()
    {
         // initiate attack coroutine when attack Input is pressed and player current state doesn't
        // equal attack
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButton("Shield") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(ShieldCo());
        }
        // updates animation and moves when player current state equals walk or equals idle
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }


    // moves position of player
    void MoveCharacter()
    {
        change.Normalize();
        playerRigidBody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
    }

    void UpdateAnimationAndMove()
    {
        // moves player and updates walking animations based on change in X and Y axis
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        // stops walking animations and player goes to walk state when not moving
        else
        {
            animator.SetBool("Moving", false);
            currentState = PlayerState.idle;
        }
    }

    private IEnumerator ShieldCo()
    {
        animator.SetBool("Shielding", true);
        currentState = PlayerState.shield;
        yield return null;
        if (!Input.GetButton("Shield"))
        {
            animator.SetBool("HoldingDown", false);
            animator.SetBool("Shielding", false);
            currentState = PlayerState.walk;
        }
        else
        {
            animator.SetBool("HoldingDown", true);
        }
    }
    // coroutine for attack to play attack animation and reset to walk after animation
    // and short delay
    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.RaiseSignal();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (playerRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            playerRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRigidBody.velocity = Vector2.zero;
        }
    }
}