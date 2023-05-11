using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class RoomMusicChange : MonoBehaviour
{
    [SerializeField]
    private IntroloopAudio clipToChangeTo;

    public void ChangeMusic()
    {
        {
            Destroy(BGSoundScript.Instance.gameObject);
            BGSoundScript.Instance.gameObject.GetComponent<IntroloopPlayer>().Play(clipToChangeTo);
        }
    }
}