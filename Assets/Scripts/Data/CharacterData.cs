using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[System.Serializable]
public class CharacterData
{
    public CharacterData(float speed, float baseAttack, float health, string playerName, bool useSavedData, bool unlocked,  int Level, float Experience, List<Ability> abilitiesList)
    {
        this.speed = speed;
        this.baseAttack = baseAttack;
        this.health = health;
        this.playerName = playerName;
        this.gearList = new List<GearData>();
        this.isUnlocked = unlocked;
        this.Level = Level;
        this.Experience = Experience;
        this.CurrentAbilitiesCharacterList = abilitiesList;

        if (useSavedData)
        {
            Load();
            
        }
    }


    public float speed;
    public float baseAttack;
    public float health;
    public string playerName;
    public List<GearData> gearList;
    public bool isUnlocked;
    public int Level;
    public float Experience;

   public List<Ability> CurrentAbilitiesCharacterList;
   public List<Ability> AllAbilitiesCharacterList;





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
        string fileName = Path.ChangeExtension(playerName, ".json");
        string path = Path.Combine(Application.persistentDataPath, fileName).Replace("\\", "/");

      
        Debug.Log("Loading data for player " + playerName);
        Debug.Log("Path to save file: " + path);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CharacterData savedData = JsonUtility.FromJson<CharacterData>(json);


            if (savedData != null)
            {
            
                speed = savedData.speed;
                baseAttack = savedData.baseAttack;
                health = savedData.health;
                gearList = savedData.gearList;
                isUnlocked = savedData.isUnlocked;
                Level = savedData.Level;
                Experience = savedData.Experience;

                CurrentAbilitiesCharacterList = savedData.CurrentAbilitiesCharacterList;




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
        string fileName = playerName + ".json";
        string path = Path.Combine(Application.persistentDataPath, fileName);        
        string json = JsonUtility.ToJson(this);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
        File.WriteAllBytes(path, bytes);


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
    public void UnlockedCharacter(bool unlock)
    {
        isUnlocked = unlock;
        CharacterDataManager.Instance.Save();
    }
    public void AddExperience(float amount)
    {
        Experience += amount;
        CheckLevelUp();
        Save();
    }

    private void CheckLevelUp()
    {
        int experienceNeeded = GetExperienceNeededForLevel(Level);
        if (Experience >= experienceNeeded)
        {
            Level++;      
            IncreaseMaxHealth(5);
            IncreaseBaseAttack(2);
            Experience -= experienceNeeded; // —охран€ем оставшийс€ опыт
            CheckLevelUp(); // –екурсивно провер€ем возможность повышени€ еще одного уровн€
        }
    }









    public int GetExperienceNeededForLevel(int level)
    {
        //  аждый новый уровень требует собрать на 10 опыта больше, чем предыдущий
        return level * 10;
    }

    public int GetCurrentLevel()
    {
        return Level;
    }

    public float GetCurrentExperience()
    {
        return Experience;
    }
}
