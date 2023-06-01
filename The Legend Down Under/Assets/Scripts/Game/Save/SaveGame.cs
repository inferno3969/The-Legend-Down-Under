using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public SaveScene newSave;
    public SaveScene prevSave;
    public SaveManager saveManager;

    public void OnEnable()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        if (prevSave != null)
        {
            prevSave.saved = false;
        }
        newSave.saved = true;
        saveManager.objects.Add(newSave);
        saveManager.SaveScriptables();
    }
}