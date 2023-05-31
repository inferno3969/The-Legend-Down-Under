using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VaatiPhase1 : BossEnemy
{
    [SerializeField] private Rigidbody2D vaatiPhase1Rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private bool summoned;
    [SerializeField] private GameObject[] shadowGuards;

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

    public AudioSource summonSound;

    // Start is called before the first frame update
    void Start()
    {
        vaatiPhase1Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        triggerCollider.enabled = false;
        animator.SetBool("Summon", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Summon") == true)
        {
            StartCoroutine(WaitCo());
        }
    }

    private void HandDownCo()
    {
        animator.SetBool("Summon", false);
        triggerCollider.enabled = true;
        nonTriggerCollider.enabled = true;
    }
    private void SummonShadowGuards()
    {
        triggerCollider.enabled = false;
        nonTriggerCollider.enabled = false;
        if (health == 10)
        {
            if (!summoned)
            {
                summonSound.Play();
                shadowGuards[0].SetActive(true);
                shadowGuards[1].SetActive(true);
                summoned = true;
            }
            if (shadowGuards[0].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[0].SetActive(false);
            }
            if (shadowGuards[1].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[1].SetActive(false);
            }
            if (shadowGuards[0].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[1].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 8 && health < 10)
        {
            if (health != 8)
            {
                health = 8;
            }
            if (!summoned)
            {
                summonSound.Play();
                shadowGuards[2].SetActive(true);
                shadowGuards[3].SetActive(true);
                summoned = true;
            }
            if (shadowGuards[2].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[2].SetActive(false);
            }
            if (shadowGuards[3].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[3].SetActive(false);
            }
            if (shadowGuards[2].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[3].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 6 && health < 8)
        {
            if (health != 6)
            {
                health = 6;
            }
            if (!summoned)
            {
                summonSound.Play();
                shadowGuards[4].SetActive(true);
                shadowGuards[5].SetActive(true);
                summoned = true;
            }
            if (shadowGuards[4].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[4].SetActive(false);
            }
            if (shadowGuards[5].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[5].SetActive(false);
            }
            if (shadowGuards[4].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[5].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 4 && health < 6)
        {
            if (health != 4)
            {
                health = 4;
            }
            if (!summoned)
            {
                summonSound.Play();
                shadowGuards[6].SetActive(true);
                shadowGuards[7].SetActive(true);
                summoned = true;
            }
            if (shadowGuards[6].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[6].SetActive(false);
            }
            if (shadowGuards[7].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[7].SetActive(false);
            }
            if (shadowGuards[6].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[7].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
        else if (health >= 2 && health < 4)
        {
            if (health != 2)
            {
                health = 2;
            }
            if (!summoned)
            {
                summonSound.Play();
                shadowGuards[8].SetActive(true);
                shadowGuards[9].SetActive(true);
                summoned = true;
            }
            if (shadowGuards[8].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[8].SetActive(false);
            }
            if (shadowGuards[9].GetComponent<ShadowGuard>().health <= 0)
            {
                shadowGuards[9].SetActive(false);
            }
            if (shadowGuards[8].GetComponent<ShadowGuard>().health <= 0 && shadowGuards[9].GetComponent<ShadowGuard>().health <= 0)
            {
                HandDownCo();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hitboxes") || other.gameObject.CompareTag("Player Projectile"))
        {
            if (health == 2)
            {
                health = 0;
                StartCoroutine(FadeCo());
            }
            summoned = false;
            animator.SetBool("Summon", true);
        }
    }

    public IEnumerator FadeCo()
    {
        newAudio = true;
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
        this.gameObject.SetActive(false);
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

    public IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(.73f);
        SummonShadowGuards();
    }
}