using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class MusicChange : MonoBehaviour
{
    public bool newAudio;
    [SerializeField] private IntroloopAudio clipToChangeTo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            if (newAudio)
            {
                BGSoundScript.Instance.gameObject.GetComponent<IntroloopPlayer>().Stop();
                BGSoundScript.Instance.gameObject.GetComponent<IntroloopPlayer>().Play(clipToChangeTo);
            }
        }
    }
}
