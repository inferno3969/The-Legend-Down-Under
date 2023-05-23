using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ContextClue : MonoBehaviour
{
    public GameObject[] contextClue;
    public bool contextActive = false;
    public SaveControllerInput savedInput;

    public void Start()
    {
        savedInput = GameObject.Find("Saved Input").GetComponent<SaveControllerInput>();
    }

    public void Update()
    {
        if (contextActive)
        {
            if (savedInput.inputType == InputType.Keyboard)
            {
                contextClue[1].SetActive(false);
                contextClue[2].SetActive(false);
                contextClue[0].SetActive(true);
            }
            if (savedInput.inputType == InputType.PlayStation)
            {
                contextClue[0].SetActive(false);
                contextClue[2].SetActive(false);
                contextClue[1].SetActive(true);
            }
            if (savedInput.inputType == InputType.Xbox)
            {
                contextClue[2].SetActive(true);
                contextClue[0].SetActive(false);
                contextClue[1].SetActive(false);
            }
        }
    }

    public void ChangeContext()
    {
        contextActive = !contextActive;
        if (contextActive)
        {
            if (savedInput.inputType == InputType.Keyboard)
            {
                contextClue[1].SetActive(false);
                contextClue[2].SetActive(false);
                contextClue[0].SetActive(true);
            }
            if (savedInput.inputType == InputType.PlayStation)
            {
                contextClue[0].SetActive(false);
                contextClue[2].SetActive(false);
                contextClue[1].SetActive(true);
            }
            if (savedInput.inputType == InputType.Xbox)
            {
                contextClue[2].SetActive(true);
                contextClue[0].SetActive(false);
                contextClue[1].SetActive(false);
            }
        }
        else
        {
            contextClue[0].SetActive(false);
            contextClue[1].SetActive(false);
            contextClue[2].SetActive(false);
        }
    }
}
