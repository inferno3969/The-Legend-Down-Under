using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public BoolValue[] saves;
    public AudioSource audioSource;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Continue()
    {
        Destroy(BGSoundScript.Instance.gameObject);
        audioSource.PlayOneShot(clickSound);
        if (saves[0].RuntimeValue)
        {
            SceneManager.LoadScene("KennysHouseCutscene");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ReturnToMainMenu()
    {
        Destroy(BGSoundScript.Instance.gameObject);
        audioSource.PlayOneShot(clickSound);

    }
}