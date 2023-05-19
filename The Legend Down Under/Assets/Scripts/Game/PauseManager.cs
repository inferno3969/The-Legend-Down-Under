using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject playerCanvas;
    public bool usingPausePanel;
    public string mainMenu;
    public GameObject resumeButton;
    public GameObject previousButton;
    public GameObject inventoryButton;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.firstSelectedGameObject = resumeButton;
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
            EventSystem.current.SetSelectedGameObject(inventoryButton);
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(previousButton);
        }
    }

}