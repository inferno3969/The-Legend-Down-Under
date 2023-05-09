using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kenny : GeneralNPC
{
    public bool interactedOnce = false;
    public bool interactedTwice = false;
    public bool insult = false;

    public Collider2D barrier;

    private PlayerFunctions player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    public override void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (player.currentState != PlayerState.Interact)
            {
                Dialog();
            }
            else
            {
                dialogBox.SetActive(false);
                player.currentState = PlayerState.Idle;
                if (interactedTwice == true)
                {
                    barrier.enabled = false;
                    interactedTwice = false;
                }
            }
        }
    }

    public override void Dialog()
    {
        dialogBox.SetActive(true);
        if (interactedOnce == false)
        {
            dialogText.text = "Hello there! I'm Kenny, Heir to the thrones of Gonder and Malkier, bearer of the sacred flame of Asgaloth, wielder of the sacred hammer Voldrung, and frequent sampler of fine cheeses and meats.";
            interactedOnce = true;
        }
        else if (interactedOnce == true && insult == false)
        {
            dialogText.text = "Behind me is the shield of the royal family. It's been passed down for generations, and is said to be unbreakable. I'm not sure if that's true, but I'm not going to test it. You can try it out for yourself if you want.";
            insult = true;
            interactedTwice = true;
        }
        else if (insult == true)
        {
            dialogText.text = "Nate is a piece of s#!t... he's always trying to make me look bad...";
        }
        player.currentState = PlayerState.Interact;
    }
}
