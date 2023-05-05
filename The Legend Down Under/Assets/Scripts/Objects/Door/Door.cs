using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                // does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    // temove a player key
                    playerInventory.numberOfKeys--;
                    // if so, then call the open method
                    Open();
                }
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
}