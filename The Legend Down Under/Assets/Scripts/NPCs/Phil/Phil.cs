using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phil : GeneralNPC
{
    [Header("Phil Properties")]
    private Rigidbody2D philRigidBody;
    private Animator animator;

    public bool nothingBought = true;
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
        dialogText.text = "If you have the PMoney coins, you can buy items from me!";
        player.currentState = PlayerState.Interact;
    }
}
