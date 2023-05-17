using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBow : MonoBehaviour
{
    public PlayerFunctions player;

    public void OnEnable()
    {
        player.receivedSpecialItemSprite = null;
    }
}
