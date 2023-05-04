using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phil : GeneralNPC
{
    [Header("Phil Properties")]
    private Rigidbody2D philRigidBody;
    private Animator animator;

    public bool nothingBought = true;
    public bool interactedOnce = false;
    public bool boughtSomething = false;
    public bool failPurchase = false;

    private PlayerFunctions player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    // Start is called before the first frame update
    void Start()
    {
        philRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (boughtSomething == false && player.currentState != PlayerState.Interact)
            {
                Dialog();
            }
            else
            {
                dialogBox.SetActive(false);
                player.currentState = PlayerState.Idle;
            }
        }
    }

    public override void Dialog()
    {
        dialogBox.SetActive(true);
        if (interactedOnce == false)
        {
            dialogText.text = "If you have the PMoney coins, you can buy items from me!";
            interactedOnce = true;
        }
        else
        {
            dialogText.text = "Say... there are four plants hidden around the map. If you find them all, I'll give you a special prize!";
            interactedOnce = false;
        }
        player.currentState = PlayerState.Interact;
    }
}
