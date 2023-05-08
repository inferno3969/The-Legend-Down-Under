using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : Interactable
{
    private PlayerFunctions player;

    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string dialog;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    public void Update()
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

    public void Dialog()
    {
        dialogBox.SetActive(true);
        dialogText.text = dialog;
        player.currentState = PlayerState.Interact;
    }
}
