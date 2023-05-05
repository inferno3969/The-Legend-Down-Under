using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;
        if (active)
        {
            ActivateLever();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange && !active)
        {
            if (!active)
            {
                ActivateLever();
                context.RaiseSignal();
                playerInRange = false;
            }
        }
    }

    public void ActivateLever()
    {
        active = true;
        storedValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !active)
        {
            context.RaiseSignal();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !active)
        {
            context.RaiseSignal();
            playerInRange = false;
        }
    }
}