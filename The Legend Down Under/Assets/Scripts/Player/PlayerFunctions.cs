using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// state machine for player
public enum PlayerState
{
    Idle,
    Walk,
    Attack,
    Stagger,
    Interact,
    Shield
}

public enum PlayerDirection
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerFunctions : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerDirection currentDirection;
    public float speed;
    public VectorValue startingPosition;

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
    public SpriteRenderer receivedNormalItemSprite;
    public SpriteRenderer receivedSpecialItemSprite;

    [Header("Weapons")]
    public InventoryItem soldierSword;
    public GameObject projectile;

    [Header("Player Hit")]
    public SignalSender playerHit;

    [Header("Invulnerability Frame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public Collider2D nonTriggerCollider;
    public SpriteRenderer playerSprite;

    [Header("Player Sword Hitboxes")]
    public GameObject playerSwordHitboxes;

    [Header("Enemy Hitboxes References")]
    public GeneralEnemy[] enemies; // best way to store enemy hitboxes in order to disable them after first hit

    // Start is called before the first frame update
    void Start()
    {
        // set current state if player to Idle
        currentState = PlayerState.Walk;
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        if (startingPosition != null)
        {
            transform.position = startingPosition.runtimeValue;
        }
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
        // is the player in an interaction
        if (currentState == PlayerState.Interact)
        {
            return;
        }
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.Attack && currentState != PlayerState.Stagger && currentState != PlayerState.Walk)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("Second Attack") && currentState != PlayerState.Attack && currentState != PlayerState.Stagger)
        {
            StartCoroutine(SecondAttackCo());
        }
        else if (Input.GetButton("Shield") && currentState != PlayerState.Attack && currentState != PlayerState.Stagger)
        {
            StartCoroutine(ShieldCo());
        }
        // updates animation and moves when player current state equals Walk or equals Idle
        else if (currentState == PlayerState.Walk || currentState == PlayerState.Idle)
        {
            UpdateAnimationAndMove();
        }
    }


    // moves position of player
    void MoveCharacter()
    {
        change.Normalize();
        playerRigidBody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
        PlayerCurrentDirection(change);
    }

    public void PlayerCurrentDirection(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                currentDirection = PlayerDirection.Right;
            }
            else if (direction.x < 0)
            {
                currentDirection = PlayerDirection.Left;
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                currentDirection = PlayerDirection.Up;
            }
            else if (direction.y < 0)
            {
                currentDirection = PlayerDirection.Down;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        // moves player and updates Walking animations based on change in X and Y axis
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        // stops Walking animations and player goes to Walk state when not moving
        else
        {
            animator.SetBool("Moving", false);
            currentState = PlayerState.Idle;
        }
    }

    private IEnumerator ShieldCo()
    {
        animator.SetBool("Shielding", true);
        currentState = PlayerState.Shield;
        yield return null;
        if (!Input.GetButton("Shield"))
        {
            animator.SetBool("HoldingDown", false);
            animator.SetBool("Shielding", false);
            currentState = PlayerState.Walk;
        }
        else
        {
            animator.SetBool("HoldingDown", true);
        }
    }
    // coroutine for Attack to play Attack animation and reset to Walk after animation
    // and short delay
    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.Attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.Interact)
        {
            currentState = PlayerState.Walk;
        }
    }

    private IEnumerator SecondAttackCo()
    {
        currentState = PlayerState.Attack;
        yield return null;
        MakeArrow();
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.Interact)
        {
            currentState = PlayerState.Walk;
        }
    }

    private void MakeArrow()
    {
        Vector2 temp = new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        ChangeArrowRotation();
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChangeArrowRotation());
    }

    // function that rotates the new spawned arrow depending on the direction the player is facing
    private Vector3 ChangeArrowRotation()
    {
        switch (currentDirection)
        {
            case PlayerDirection.Up:
                return new Vector3(0, 0, 180);
            case PlayerDirection.Down:
                return new Vector3(0, 0, 0);
            case PlayerDirection.Left:
                return new Vector3(0, 0, -90);
            case PlayerDirection.Right:
                return new Vector3(0, 0, 90);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.RaiseSignal();
        if (currentHealth.RuntimeValue > 0 && this.gameObject != null)
        {

            StartCoroutine(KnockCo(knockTime, damage));
        }
        else
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator KnockCo(float knockTime, float damage)
    {
        playerHit.RaiseSignal();
        if (playerRigidBody != null)
        {
            if (damage > 0)
            {
                StartCoroutine(FlashCo());
            }
            yield return new WaitForSeconds(knockTime);
            playerRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.Idle;
            playerRigidBody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        if (enemies != null)
        {
            DisableEnemyHitboxes();
        }
        int tempFlashes;
        // turn off player trigger collider to prevent from taking damage
        triggerCollider.enabled = false;
        // prevent player from sliding when hit by an enemy
        nonTriggerCollider.enabled = false;
        // go through numberOfFlashes while iterating tempFlashes
        for (tempFlashes = 0; tempFlashes < numberOfFlashes; tempFlashes++)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
        }
        // set trigger collider back on when for loop is finished
        triggerCollider.enabled = true;
        nonTriggerCollider.enabled = true;

        if (enemies != null)
        {
            EnableEnemyHitboxes();
        }
    }

    private void DisableEnemyHitboxes()
    {
        foreach (GeneralEnemy enemy in enemies)
        {
            enemy.enemyHitboxes.SetActive(false);
        }
    }

    private void EnableEnemyHitboxes()
    {
        foreach (GeneralEnemy enemy in enemies)
        {
            enemy.enemyHitboxes.SetActive(true);
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.Interact)
            {
                if (playerInventory.currentItem.unique == false)
                {
                    animator.SetBool("NormalReceive", true);
                    receivedNormalItemSprite.sprite = playerInventory.currentItem.itemImage;
                }
                else if (playerInventory.currentItem.unique == true)
                {
                    animator.SetBool("SpecialReceive", true);
                    receivedSpecialItemSprite.sprite = playerInventory.currentItem.itemImage;
                }
                currentState = PlayerState.Interact;
            }
        }
    }
}