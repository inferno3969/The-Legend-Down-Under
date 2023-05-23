using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FloatValue[] playerHealth;
    public BoolValue[] otherScriptableObjects;
    public PlayerInventory playerInventory;

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
        Destroy(BGSoundScript.Instance.gameObject);
        SceneManager.LoadScene("LinksBedroomCutscene");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}