using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    public AudioSource buttonSound;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnSelect(BaseEventData eventData)
    {
        HoverSound();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        ClickSound();
    }

    public void HoverSound()
    {
        buttonSound.PlayOneShot(hoverSound);
    }

    public void ClickSound()
    {
        buttonSound.PlayOneShot(clickSound);
    }
}
