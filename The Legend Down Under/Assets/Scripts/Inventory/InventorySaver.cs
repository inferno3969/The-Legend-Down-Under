using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory myInventory;

    private void OnStart()
    {
        myInventory.myInventory.Clear();
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    private void OnApplicationQuit()
    {
        SaveScriptables();
    }

    public void ResetScriptables()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath +
            string.Format($"/{i}.inv", i)))
        {
            File.Delete(Application.persistentDataPath +
                string.Format($"/{i}.inv", i));
            i++;
        }

    }

    public void SaveScriptables()
    {
        ResetScriptables();
        for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format($"/{i}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(myInventory.myInventory[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath +
            string.Format($"/{i}.inv", i)))
        {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();
            FileStream file = File.Open(Application.persistentDataPath +
                string.Format($"/{i}.inv", i), FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                temp);
            file.Close();
            myInventory.myInventory.Add(temp);
            i++;
        }

    }
}