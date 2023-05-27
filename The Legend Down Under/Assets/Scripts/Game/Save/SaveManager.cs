using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public BoolValue saveGame;
    public bool saved;

    public void OnEnable()
    {
        saved = true;
        saveGame.RuntimeValue = saved;
    }
}
