using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public float speed;
    public float baseAttack;
    public float health;
    public int coins;
    public string playerName;


    public PlayerData(float speed, float baseAttack, float health, int coins, string playerName)
    {
        this.speed = speed;
        this.baseAttack = baseAttack;
        this.health = health;
        this.coins = coins;
        this.playerName = playerName;
    }

    public void Load()
    {
        string path = Application.dataPath + "/Data/" + playerName + ".json";

        //  string path = Application.persistentDataPath + "/Data/" + playerName + ".json";

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else
        {
            Debug.Log("No save file found for player " + playerName);
        }
    }

    public void Save()
    {
        ///  для сохранения в приложении
      //  string path = Application.persistentDataPath + "/" + playerName + ".json";
        string path = Application.dataPath + "/Data/" + playerName + ".json";

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
