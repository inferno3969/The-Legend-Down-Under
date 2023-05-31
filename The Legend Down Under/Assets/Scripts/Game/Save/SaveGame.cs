using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public BoolValue saveGame;
    public bool saved;
    public SaveManager saveManager;

    public void OnEnable()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        saved = true;
        saveGame.RuntimeValue = saved;
        saveManager.objects.Add(saveGame);
        saveManager.SaveScriptables();
    }
}