using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public FloatValue[] playerHealth;
    public BoolValue[] otherScriptableObjects;
    public PlayerInventory playerInventory;
    public BoolValue[] saves;
    public GameObject[] scenes;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public GameObject newGameButton;
    public GameObject quitButton;
    public Button quitButton2;
    public Button newGameButton2;

    public GameObject promptPanel;
    public GameObject areYouSurePrompt;
    public GameObject yesButton;
    public GameObject noButton;


    public SaveManager saveManager;

    public Button continueButton;

    public void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        saveManager.LoadScriptables();
        if (saves[0].RuntimeValue)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void NewGame()
    {
        // remove all items from inventory
        playerInventory.myInventory.Clear();
        playerInventory.numberOfArrows = 0;
        playerInventory.numberOfBossKeys = 0;
        playerInventory.numberOfKeys = 0;
        foreach (FloatValue floatValue in playerHealth)
        {
            floatValue.RuntimeValue = floatValue.initialValue;
        }
        foreach (BoolValue boolValue in otherScriptableObjects)
        {
            boolValue.RuntimeValue = boolValue.initialValue;
        }
        foreach (BoolValue boolValue in saves)
        {
            boolValue.RuntimeValue = boolValue.initialValue;
        }
        saveManager.objects.Clear();
        audioSource.PlayOneShot(clickSound);
        Destroy(BGSoundScript.Instance.gameObject);
        SceneManager.LoadScene("LinksBedroomCutscene");
    }

    public void Continue()
    {
        audioSource.PlayOneShot(clickSound);
        if (saves[0].RuntimeValue && !saves[1].RuntimeValue)
        {
            scenes[0].SetActive(true);
        }
        else if (saves[1].RuntimeValue && saves[0].RuntimeValue)
        {
            scenes[1].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitPrompt()
    {
        quitButton2.interactable = false;
        newGameButton2.interactable = false;
        continueButton.interactable = false;
        audioSource.PlayOneShot(clickSound);
        promptPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(quitButton);
    }

    public void AreYouSurePrompt()
    {
        promptPanel.SetActive(false);
        audioSource.PlayOneShot(clickSound);
        areYouSurePrompt.SetActive(true);
        EventSystem.current.SetSelectedGameObject(noButton);
    }

    public void CancelQuit()
    {
        quitButton2.interactable = true;
        newGameButton2.interactable = true;
        continueButton.interactable = true;
        audioSource.PlayOneShot(clickSound);
        areYouSurePrompt.SetActive(false);
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}