using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FloatValue[] playerHealth;
    public BoolValue[] otherScriptableObjects;
    public PlayerInventory playerInventory;
    public BoolValue[] saves;
    public GameObject[] scenes;
    public AudioSource audioSource;
    public AudioClip clickSound;

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
        audioSource.PlayOneShot(clickSound);
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

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}