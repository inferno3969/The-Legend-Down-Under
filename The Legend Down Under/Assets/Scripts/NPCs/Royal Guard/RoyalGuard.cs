using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalGuard : GeneralNPC
{
    private PlayerFunctions player;
    public string dialog;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    // Start is called before the first frame update
    void Start()
    {

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
        dialogText.text = dialog;
        player.currentState = PlayerState.Interact;
    }
}
