using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreItem : Interactable
{
    public string itemName;
    public int cost;
    public PlayerInventory playerInventory;
    public Phil phil;
    public PlayerFunctions player;
    public SignalSender coinSignal;

    [Header("Dialog")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    bool attemptPurchase = false;

    private void Awake()
    {
        phil = GameObject.FindGameObjectWithTag("Phil").GetComponent<Phil>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (attemptPurchase == false && player.currentState != PlayerState.Interact)
            {
                BuyItem(itemName, cost, playerInventory.coins);
                attemptPurchase = true;
            }
            else if (attemptPurchase == true && player.currentState == PlayerState.Interact)
            {
                phil.GetComponent<Animator>().SetBool("BoughtSomething", false);
                phil.GetComponent<Animator>().SetBool("FailedPurchase", false);
                dialogBox.SetActive(false);
                player.currentState = PlayerState.Idle;
                attemptPurchase = false;
            }
        }
    }

    public void BuyItem(string name, int cost, int coins)
    {
        if (coins >= cost)
        {
            Success();
        }
        else
        {
            Fail();
        }
        player.currentState = PlayerState.Interact;
    }

    public void Success()
    {
        phil.GetComponent<Animator>().SetBool("BoughtSomething", true);
        dialogBox.SetActive(true);
        dialogText.text = "Thank you for your purchase!";
        playerInventory.coins -= 1;
        coinSignal.RaiseSignal();
    }

    public void Fail()
    {
        phil.GetComponent<Animator>().SetBool("FailedPurchase", true);
        dialogBox.SetActive(true);
        dialogText.text = "You don't have enough PMoney coins!";
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
            playerInRange = false;
            context.RaiseSignal();
        }
    }
}
