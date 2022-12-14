using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public string SavePath => $"{Application.persistentDataPath}/save.txt";

    //Function to save data. (Can be called from Unity)
    [ContextMenu("save")]
    public void Save()
    {
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
    }

    //Function to load earlier save data. (Can be called from Unity)
    [ContextMenu("load")]
    public void Load()
    {
        var state = LoadFile();
        LoadState(state);
    }

    //Function to generate Id's for all saveable objects. (Can be called from Unity)
    [ContextMenu("GenerateIdForAllObjects")]
    public void GenerateIdForAllObjects()
    {
        foreach(var saveable in FindObjectsOfType<SaveableEntity>())
        {
            saveable.GenerateId();
            print("Generated Id: " + saveable.Id + " for object " + saveable.name);
        }

    }

    public void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found");
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state)
    {
        foreach(var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.SaveState();
        }
    }
    void LoadState(Dictionary<string, object> state)
    {
        foreach(var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.Id,out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
