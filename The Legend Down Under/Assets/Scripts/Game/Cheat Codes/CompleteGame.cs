using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CompleteGame : MonoBehaviour
{
    private string[] cheatCode;
    private int index;

    private int[] values;
    private bool[] keys;

    public GameObject[] buttons;

    public GameObject blackImage;

    void Start()
    {
        // Code is "idkfa", user needs to input this in the right order
        cheatCode = new string[] { "Cheat1", "Cheat2", "Cheat3", "Cheat4", "Cheat5", "Cheat6" };
        index = 0;
    }

    // void Update()
    // {
    //     foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
    //     {
    //         if (Input.GetKeyDown(vKey))
    //         {
    //             Debug.Log("KeyCode down: " + vKey);
    //         }
    //     }
    // }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Check if the next key in the code is pressed
            if (Input.GetButtonDown(cheatCode[index]))
            {
                // Add 1 to index to check the next key in the code
                index++;
            }
            // Wrong key entered, we reset code typing
            else
            {
                index = 0;
            }
        }

        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == cheatCode.Length)
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }
            blackImage.SetActive(true);
            // Cheat code successfully inputted!
            // Unlock crazy cheat code stuff
            SceneManager.LoadScene("FinalStageCutscene2");
        }
    }
}
