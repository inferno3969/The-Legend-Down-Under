using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class SaveScene : ScriptableObject
{
    public bool saved;
    public int sceneIndex;
}