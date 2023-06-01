using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject[] scenes;
    public AudioSource audioSource;
    public AudioClip clickSound;

    private int currentSaveIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSaveIndex = PlayerPrefs.GetInt("currentSaveIndex", 0);
    }

    public void Update()
    {
        Debug.Log(currentSaveIndex);
    }

    public void Continue()
    {
        audioSource.PlayOneShot(clickSound);
        scenes[currentSaveIndex].SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        Destroy(BGSoundScript.Instance.gameObject);
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene("MainMenu");
    }

    public void CurrentSave(int index)
    {
        currentSaveIndex = index;
        PlayerPrefs.SetInt("currentSaveIndex", currentSaveIndex);
        Debug.Log(currentSaveIndex);
    }
}