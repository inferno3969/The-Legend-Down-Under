using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivationAfterCutscene : MonoBehaviour
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
    public bool newAudio;

    void OnEnable()
    {
        playerStorage.runtimeValue = playerPosition;
        StartCoroutine(FadeCo());
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        if (newAudio)
        {
            Destroy(BGSoundScript.Instance.gameObject);
        }
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {
        cameraMax.runtimeValue = cameraNewMax;
        cameraMin.runtimeValue = cameraNewMin;
    }
}