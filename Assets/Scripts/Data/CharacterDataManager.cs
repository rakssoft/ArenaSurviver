using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterDataManager : MonoBehaviour
{
    public List<CharacterData> characterDataList = new List<CharacterData>();
    [SerializeField] private CharacterManager _characterManager;
    private CharacterData _characterData;
    public static CharacterDataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Загружаем сохраненные данные
        Load();
       Save();
    }

    private void OnDisable()
    {
        Save();
    }

    public void AddPlayer(string playerName, CharacterData characterData)
    {
        // Добавляем игрока в список
        characterData.playerName = playerName;
        characterDataList.Add(characterData);
    }

    public CharacterData GetPlayerData(string playerName)
    {
        return characterDataList.FirstOrDefault(player => player.playerName == playerName);
    }

    public bool PlayerExists(string playerName)
    {
        foreach (CharacterData characterData in characterDataList)
        {
            if (characterData.playerName == playerName)
            {
                return true;
            }
        }
        return false;
    }

    public void Load()
    {
        characterDataList.Clear();

        // Загружаем все сохраненные файлы
        string[] saveFiles = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.json");

        // Если сохранений нет, создаем новых персонажей из списка
        if (saveFiles.Length == 0)
        {
            foreach (CharacterCharacteristics characterCharacteristics in _characterManager._characterCharacteristics)
            {
                _characterData = new CharacterData(characterCharacteristics.Speed, characterCharacteristics.BaseAttack,
                    characterCharacteristics.MaxHealth, characterCharacteristics.Name, false, characterCharacteristics.Unlocked,
                    characterCharacteristics.Level, characterCharacteristics.Experience, characterCharacteristics.CharacterAbilities);
                characterDataList.Add(_characterData);
            }
        }
        else
        {
            foreach (string saveFile in saveFiles)
            {
                string json = System.IO.File.ReadAllText(saveFile);
                CharacterData characterData = JsonUtility.FromJson<CharacterData>(json);
                characterDataList.Add(characterData);
            }
        }

        // Назначаем данные персонажам
        foreach (CharacterCharacteristics characterCharacteristics in FindObjectsOfType<CharacterCharacteristics>())
        {
            CharacterData playerData = GetPlayerData(characterCharacteristics.Name);
            if (playerData != null)
            {
                characterCharacteristics.SetPlayerData(playerData);
            }
        }
    }

    public void Save()
    {
      
        foreach (CharacterData characterData in characterDataList)
        {
            characterData.Save();
        }
    }
}
