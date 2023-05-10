using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DoorType
{
    key,
    enemy,
    button,
    lever
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public PlayerInventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public PlayerFunctions player;

    [Header("Dialog")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (playerInRange && thisDoorType == DoorType.key && player.currentState != PlayerState.Interact)
            {
                player.currentState = PlayerState.Interact;
                // does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    // temove a player key
                    playerInventory.numberOfKeys--;
                    // if so, then call the open method
                    Open();
                }
                else if (playerInventory.numberOfKeys <= 0)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = "You need a key to open this door.";
                }
            }
            if (playerInRange && thisDoorType == DoorType.button && player.currentState != PlayerState.Interact)
            {
                player.currentState = PlayerState.Interact;
                // does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    // temove a player key
                    playerInventory.numberOfKeys--;
                    // if so, then call the open method
                    Open();
                    context.RaiseSignal();
                    playerInRange = false;
                    player.currentState = PlayerState.Idle;
                }
                else if (playerInventory.numberOfKeys <= 0)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = "You need a key to open this door.";
                }
            }
            else if (player.currentState == PlayerState.Interact)
            {
                dialogBox.SetActive(false);
                player.currentState = PlayerState.Idle;
            }
        }
    }

    public void Open()
    {
        // turn off the door's sprite renderer
        doorSprite.enabled = false;
        //set open to true
        open = true;
        //turn off the door's box collider
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        // turn off the door's sprite renderer
        doorSprite.enabled = true;
        //set open to true
        open = false;
        // turn off the door's box collider
        physicsCollider.enabled = true;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !open)
        {
            context.RaiseSignal();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !open)
        {
            playerInRange = false;
            context.RaiseSignal();
        }
    }
}