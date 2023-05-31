using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSFX : MonoBehaviour
{
    public AudioSource deathSFX;

    public void OnEnable()
    {
        deathSFX.Play();
    }
}
