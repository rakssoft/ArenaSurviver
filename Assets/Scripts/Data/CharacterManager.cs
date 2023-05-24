using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterCharacteristics> _characterCharacteristics;

    private void Start()
    {
        if (_characterCharacteristics.Count > 0)
        {
            foreach (var playerCharacteristics in _characterCharacteristics)
            {
                CharacterData characterData = CharacterDataManager.Instance.GetPlayerData(playerCharacteristics.Name);
                if (characterData == null)
                {
                    // ���� ������ ��� � ������, ������� �����, ��������� �������� �� PlayerCharacteristics
                    characterData = new CharacterData(playerCharacteristics.Speed, playerCharacteristics.BaseAttack, 
                        playerCharacteristics.MaxHealth, playerCharacteristics.Name, false, playerCharacteristics.Unlocked,
                        playerCharacteristics.Level, playerCharacteristics.Experience, playerCharacteristics.CharacterAbilities);
                    CharacterDataManager.Instance.AddPlayer(playerCharacteristics.Name, characterData);
                }
                playerCharacteristics.SetPlayerData(characterData);
            }
        }
        else
        {
            Debug.Log("No characters");
        }
    }

    private void OnDisable()
    {
        CharacterDataManager.Instance.Save();
    }


    public void Upgrade(CharacterCharacteristics Character, float Experience)
    {
        // �������� ������ �������� ������
        CharacterData characterData = CharacterDataManager.Instance.GetPlayerData(Character.Name);

        // �������� �������� �������������
        /*        playerData.IncreaseMaxHealth(healthIncrease);
                playerData.IncreaseBaseAttack(damageIncrease);
                playerData.IncreaseSpeed(speedIncrease);*/
        characterData.AddExperience(Experience);
        // ��������� ������ �� ������� �������
        Character.SetPlayerData(characterData);

        // ��������� ���������� ������
        characterData.Save();
        CharacterDataManager.Instance.Save();
    }


}
