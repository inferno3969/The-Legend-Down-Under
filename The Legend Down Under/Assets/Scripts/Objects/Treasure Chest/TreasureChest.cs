using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public InventoryItem contents;
    public PlayerInventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signals")]
    public SignalSender raiseItem;

    [Header("Dialog")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    [Header("Animator")]
    private Animator animator;

    private PlayerFunctions player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            animator.SetBool("Opened", true);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.RaiseSignal();
        context.RaiseSignal();
        isOpen = true;
        animator.SetBool("Opened", true);
        storedOpen.RuntimeValue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
        raiseItem.RaiseSignal();
        if (Input.GetButton("Interact"))
        {
            player.GetComponent<Animator>().SetBool("NormalReceive", false);
            player.GetComponent<Animator>().SetBool("SpecialReceive", false);
            player.currentState = PlayerState.Idle;
            player.receivedNormalItemSprite.sprite = null;
            player.receivedSpecialItemSprite.sprite = null;
            playerInventory.currentItem = null;
        }
        playerInRange = false;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.RaiseSignal();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.RaiseSignal();
            playerInRange = false;
        }
    }
}
