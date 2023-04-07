using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<Gear> gearList;

    public void Save()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path, json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            InventoryData savedData = JsonUtility.FromJson<InventoryData>(json);
            if (savedData != null)
            {
                gearList = savedData.gearList;
      
            }
            else
            {
                Debug.Log("Failed to load inventory data from file.");
            }
        }
        else
        {
            Debug.Log("No save file found for inventory.");
        }
    }
}
