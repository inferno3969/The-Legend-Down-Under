using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoreItemScene : Interactable
{
    public string itemName;
    public int quantity;
    public int cost;
    public PlayerInventory playerInventory;
    public Phil phil;
    public PlayerFunctions player;
    public SignalSender coinSignal;
    public InventoryItem item;
    public GameObject itemObject;

    [Header("Dialog")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    [Header("New Scene Variables")]
    public string sceneToLoad;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    bool attemptPurchase = false;
    bool successfullPurchase = false;
    public bool isPlant = false;

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
                if (successfullPurchase == true)
                {
                    Destroy(itemObject);
                    successfullPurchase = false;
                }
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
            StartCoroutine(FadeCo());
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
        if (isPlant == false)
        {
            dialogText.text = "Phil: Thank you for your purchase!";
        }
        playerInventory.coins -= cost;
        coinSignal.RaiseSignal();
        playerInventory.AddItem(item);
        item.numberHeld += quantity;
        successfullPurchase = true;
    }

    public void Fail()
    {
        phil.GetComponent<Animator>().SetBool("FailedPurchase", true);
        dialogBox.SetActive(true);
        int costDifference = cost - playerInventory.coins;
        if (costDifference > 1)
        {
            dialogText.text = "Phil: You don't have enough PMoney coins! You need " + costDifference + " coins to buy this item.";
        }
        else
        {
            dialogText.text = "Phil: You don't have enough PMoney coins! You need " + costDifference + " coin to buy this item.";
        }
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

    public IEnumerator FadeCo()
    {
        player.currentState = PlayerState.Interact;
        yield return new WaitForSeconds(4f);
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
