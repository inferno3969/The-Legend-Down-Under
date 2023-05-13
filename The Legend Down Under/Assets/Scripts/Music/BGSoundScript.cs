using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class BGSoundScript : MonoBehaviour
{
    [SerializeField] private static BGSoundScript instance = null;
    public static BGSoundScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}