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

    private int sceneIndex;

    public SaveManager saveManager;

    public Button continueButton;

    public void Awake()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        saveManager.LoadScriptables();
    }

    public void Start()
    {
        for (int i = 0; i < saveManager.objects.Count; i++)
        {
            if (saveManager.objects[i].GetType() == typeof(SaveScene))
            {
                Debug.Log("found");
                SaveScene temp = (SaveScene)saveManager.objects[i];
                if (temp.saved)
                {
                    playerHealth[1].RuntimeValue = playerHealth[1].initialValue;
                    continueButton.interactable = true;
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }
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
        foreach (ScriptableObject scriptableObject in saveManager.objects)
        {
            if (scriptableObject.GetType() == typeof(SaveScene))
            {
                SaveScene temp = (SaveScene)scriptableObject;
                temp.saved = false;
            }
            if (scriptableObject.GetType() == typeof(FloatValue))
            {
                FloatValue temp = (FloatValue)scriptableObject;
                temp.RuntimeValue = temp.initialValue;
            }
        }
        saveManager.objects.Clear();
        saveManager.ResetScriptables();
        audioSource.PlayOneShot(clickSound);
        Destroy(BGSoundScript.Instance.gameObject);
        SceneManager.LoadScene("LinksBedroomCutscene");
    }

    public void Continue()
    {
        playerInventory.numberOfBossKeys = 0;
        playerInventory.numberOfKeys = 0;
        foreach (BoolValue boolValue in otherScriptableObjects)
        {
            boolValue.RuntimeValue = boolValue.initialValue;
        }
        audioSource.PlayOneShot(clickSound);
        saveManager.ResetScriptables();
        foreach (ScriptableObject scriptableObject in saveManager.objects)
        {
            if (scriptableObject.GetType() == typeof(SaveScene))
            {
                SaveScene temp = (SaveScene)scriptableObject;
                if (temp.saved)
                {
                    GameOver sceneIndex = new GameOver();
                    Debug.Log(temp.sceneIndex);
                    scenes[temp.sceneIndex].SetActive(true);
                    sceneIndex.CurrentSave(temp.sceneIndex);
                    Debug.Log(temp.sceneIndex);

                }
            }
        }
    }

    public void NewGamePrompt()
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

    public void CancelNewGame()
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