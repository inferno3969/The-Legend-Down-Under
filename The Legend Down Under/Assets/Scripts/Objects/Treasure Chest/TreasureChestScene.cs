using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TreasureChestScene : Interactable
{
    [Header("Contents")]
    public InventoryItem contents;
    public PlayerInventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signals")]
    public SignalSender raiseItem;

    [Header("Dialog")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    [Header("Animator")]
    private Animator animator;

    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;
    public VectorValue cameraMin;
    public VectorValue cameraMax;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    public bool newAudio;

    private PlayerFunctions player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            animator.SetBool("Opened", true);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.RaiseSignal();
        context.RaiseSignal();
        isOpen = true;
        animator.SetBool("Opened", true);
        storedOpen.RuntimeValue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
        playerStorage.runtimeValue = playerPosition;
        dialogBox.SetActive(false);
        raiseItem.RaiseSignal();
        if (Input.GetButton("Interact"))
        {
            player.GetComponent<Animator>().SetBool("NormalReceive", false);
            player.GetComponent<Animator>().SetBool("SpecialReceive", false);
            player.currentState = PlayerState.Idle;
            player.receivedNormalItemSprite.sprite = null;
            player.receivedSpecialItemSprite.sprite = null;
            playerInventory.currentItem = null;
        }
        playerInRange = false;
        StartCoroutine(FadeCo());
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.RaiseSignal();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.RaiseSignal();
            playerInRange = false;
        }
    }

    public IEnumerator FadeCo()
    {
        player.currentState = PlayerState.Interact;
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        if (newAudio)
        {
            Destroy(BGSoundScript.Instance.gameObject);
        }
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {
        cameraMax.runtimeValue = cameraNewMax;
        cameraMin.runtimeValue = cameraNewMin;
        context.RaiseSignal();
        playerInRange = false;
    }
}
