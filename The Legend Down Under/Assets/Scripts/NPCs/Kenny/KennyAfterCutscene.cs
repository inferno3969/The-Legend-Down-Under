using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KennyAfterCutscene : GeneralNPC
{
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
            }
        }
    }

    public override void Dialog()
    {
        dialogBox.SetActive(true);
        dialogText.text = "Kenny: Nate is a piece of s#!t... he's always trying to make me look bad...";
        player.currentState = PlayerState.Interact;
    }
}