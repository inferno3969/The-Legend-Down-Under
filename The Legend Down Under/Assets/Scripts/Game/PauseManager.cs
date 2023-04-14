using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject playerCanvas;
    public bool usingPausePanel;
    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        usingPausePanel = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            ChangePause();
        }

    }

    public void ChangePause()
    {
        playerCanvas.SetActive(false);
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            usingPausePanel = true;
        }
        else
        {
            inventoryPanel.SetActive(false);
            pausePanel.SetActive(false);
            playerCanvas.SetActive(true);
            Time.timeScale = 1f;
            usingPausePanel = false;
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }

}