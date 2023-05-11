using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using E7.Introloop;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public TextMeshProUGUI placeText;
    [SerializeField] private IntroloopAudio clipToChangeTo;
    public bool newAudio;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (newAudio)
            {
                BGSoundScript.Instance.gameObject.GetComponent<IntroloopPlayer>().Stop();
                BGSoundScript.Instance.gameObject.GetComponent<IntroloopPlayer>().Play(clipToChangeTo);
            }
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (text != null)
            {
                if (needText == true)
                {
                    StartCoroutine(placeNameCo());
                }
                else if (needText == false)
                {
                    text.SetActive(false);
                }
            }
        }
    }


    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
}