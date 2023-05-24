using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private static PlayerData instance = null;
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerData();
                instance.Load();
            }
            return instance;
        }
    }

    public int Level;
    public float Experience;

    private PlayerData()
    {
        Level = 1;
        Experience = 0.0f;
    }

    public void AddExperience(float amount)
    {
        Experience += amount;
        CheckLevelUp();
        Save();
    }

    private void CheckLevelUp()
    {
        int experienceNeeded = GetExperienceNeededForLevel(Level );
        if (Experience >= experienceNeeded)
        {
            Level++;
            Experience -= experienceNeeded; // ��������� ���������� ����
            CheckLevelUp(); // ���������� ��������� ����������� ��������� ��� ������ ������
        }
    }

    public int GetExperienceNeededForLevel(int level)
    {
        // ������ ����� ������� ������� ������� �� 10 ����� ������, ��� ����������
        return level * 10;
    }


    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("playerData", json);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("playerData"))
        {
            string json = PlayerPrefs.GetString("playerData");
            PlayerData savedPlayerData = JsonUtility.FromJson<PlayerData>(json);
            Level = savedPlayerData.Level;
            Experience = savedPlayerData.Experience;
        }
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
