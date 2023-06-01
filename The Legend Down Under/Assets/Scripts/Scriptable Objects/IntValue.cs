using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    public int initialValue;
    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        initialValue = RuntimeValue;
    }

    public void OnBeforeSerialize()
    {

    }
}