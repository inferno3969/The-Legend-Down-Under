using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralNPC : Interactable
{
    [Header("NPC Information")]
    public string npcName;

    [Header("Dialog")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    public virtual void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Dialog();
        }
    }

    public virtual void Dialog()
    {
        dialogBox.SetActive(true);
        dialogText.text = name;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.RaiseSignal();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.RaiseSignal();
            playerInRange = false;
        }
    }
}
