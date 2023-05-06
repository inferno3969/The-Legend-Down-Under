using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableSceneTransition : Interactable
{

    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;
    public VectorValue cameraMin;
    public VectorValue cameraMax;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    public PlayerFunctions player;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            playerStorage.runtimeValue = playerPosition;
            player.animator.SetBool("Moving", false);
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        player.currentState = PlayerState.Interact;
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {

        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
        context.RaiseSignal();
        playerInRange = false;
    }
}