using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playerCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        Destroy(BGSoundScript.Instance.gameObject);
        SceneManager.LoadScene("LinksBedroomCutscene");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}