using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerDataManager : MonoBehaviour
{
    public List<PlayerData> playerDataList = new List<PlayerData>();

    private void Awake()
    {
        Load();
    }

    private void OnDisable()
    {
        Save();
    }

    public void AddPlayer(string playerName, PlayerData playerData)
    {
        // Добавляем игрока в список
        playerData.playerName = playerName;
        playerDataList.Add(playerData);
    }


    public PlayerData GetPlayerData(string playerName)
    {
        return playerDataList.FirstOrDefault(player => player.playerName == playerName);
    }

    public bool PlayerExists(string playerName)
    {
        foreach (PlayerData playerData in playerDataList)
        {
            if (playerData.playerName == playerName)
            {
                return true;
            }
        }
        return false;
    }


    public void Load()
    {
        playerDataList.Clear();

        /// прикомпиляции сделать сохранение так.
      //  string[] saveFiles = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.json");
        string[] saveFiles = System.IO.Directory.GetFiles(Application.dataPath + "/Data/",  "*.json");


        foreach (string saveFile in saveFiles)
        {
            string json = System.IO.File.ReadAllText(saveFile);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            playerDataList.Add(playerData);
        }
    }

    public void Save()
    {
        foreach (PlayerData playerData in playerDataList)
        {
            playerData.Save();
        }
    }
}
