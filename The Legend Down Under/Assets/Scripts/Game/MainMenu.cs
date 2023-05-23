using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FloatValue[] playerHealth;
    public BoolValue[] otherScriptableObjects;

    public void NewGame()
    {
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