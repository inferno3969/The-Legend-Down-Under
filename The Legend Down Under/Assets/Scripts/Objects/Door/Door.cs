using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DoorType
{
    Key,
    Enemy,
    Button,
    Lever,
    Boss
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public PlayerInventory playerInventory;
    public SpriteRenderer doorSprite;
    public Collider2D physicsCollider;
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
            if (player.currentState != PlayerState.Interact)
            {
                player.currentState = PlayerState.Interact;
                if (playerInRange && thisDoorType == DoorType.Key && !open)
                {
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
                        dialogText.text = "You need a small key to open this door.";
                    }
                }
                else if (playerInRange && thisDoorType == DoorType.Lever && !open)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = "A lever needs to be activated to open this door.";
                }
                else if (playerInRange && thisDoorType == DoorType.Button && !open)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = "A switch needs to be pushed to open this door.";
                }
                else if (playerInRange && thisDoorType == DoorType.Boss && !open)
                {
                    // does the player have a key?
                    if (playerInventory.numberOfBossKeys > 0)
                    {
                        // temove a player key
                        playerInventory.numberOfBossKeys--;
                        // if so, then call the open method
                        Open();
                        context.RaiseSignal();
                        playerInRange = false;
                        player.currentState = PlayerState.Idle;
                    }
                    else if (playerInventory.numberOfBossKeys <= 0)
                    {
                        dialogBox.SetActive(true);
                        dialogText.text = "You need a boss key to open this door.";
                    }
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