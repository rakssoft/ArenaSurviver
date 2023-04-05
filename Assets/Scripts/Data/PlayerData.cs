using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    // Добавляем новый параметр для загрузки данных из сохранения
    public PlayerData(float speed, float baseAttack, float health, int coins, string playerName, bool useSavedData)
    {
        if (useSavedData)
        {
            Load();
        }
        else
        {
            this.speed = speed;
            this.baseAttack = baseAttack;
            this.health = health;
            this.coins = coins;
            this.playerName = playerName;
            this.gearList = new List<GearData>();
        }
    

        this.playerName = playerName;
        this.gearList = new List<GearData>();
    }

    public float speed;
    public float baseAttack;
    public float health;
    public int coins;
    public string playerName;
    public List<GearData> gearList; // Список одежды


    public void AddGear(GearData gear)
    {
        gearList.Add(gear);
        Save();
    }

    public void RemoveGear(GearData gear)
    {
        gearList.Remove(gear);
        Save();
    }

    public void Load()
    {
        string fileName = playerName + ".json";
        string path = Application.persistentDataPath + "/" + fileName;

   //     string path = Application.dataPath + "/Data/" + playerName + ".json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData savedData = JsonUtility.FromJson<PlayerData>(json);
            if (savedData != null)
            {
                speed = savedData.speed;
                baseAttack = savedData.baseAttack;
                health = savedData.health;
                coins = savedData.coins;
                gearList = savedData.gearList;       
            }
            else
            {
                Debug.Log("Failed to load player data from file: " + playerName);
            }
        }
        else
        {
            Debug.Log("No save file found for player " + playerName);
        }
    }




    public void Save()
    {
        string path = Application.persistentDataPath + "/" + playerName + ".json";
              
        //  string path = Application.dataPath + "/Data/" + playerName + ".json";
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path, json);
    }

    public void IncreaseMaxHealth(float amount)
    {
        health += amount;
    }

    public void IncreaseBaseAttack(float amount)
    {
        baseAttack += amount;
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    public void DecreaseCoins(int amount)
    {
        coins -= amount;
    }

}
