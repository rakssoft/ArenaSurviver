using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private CharacterCharacteristics _character;
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


    /// <summary>
    /// ������� ��������� ��������� ����� � ������ ������.
    /// </summary>
    /// <param name="Character"></param>
    /// <param name="Experience"></param>
    public void Upgrade(CharacterCharacteristics Character, float Experience)
    {
        // �������� ������ �������� ������
        _character = Character;
        CharacterData characterData = CharacterDataManager.Instance.GetPlayerData(Character.Name);
        
        // �������� �������� �������������
        /*        playerData.IncreaseMaxHealth(healthIncrease);
                playerData.IncreaseBaseAttack(damageIncrease);
                playerData.IncreaseSpeed(speedIncrease);*/
        characterData.AddExperience(Experience);
        // ��������� ������ �� ������� �������
        Character.SetPlayerData(characterData);
        UpdateAbilitiesOnLevelUp();
        // ��������� ���������� ������
        characterData.Save();
        CharacterDataManager.Instance.Save();
    }


    /// <summary>
    /// � ����������� � �������� ����������� ��� ������  3. 7. 10  ����� ��������� 
    /// </summary>
    private void UpdateAbilitiesOnLevelUp()
    {
        int level = _character.Level;
        if (level == 3 && !_character.CharacterAbilities.Any(a => GetAbilityLevel(a) == 1))
        {
            AddAbilityByLevel(1);
        }
        if (level == 7 && !_character.CharacterAbilities.Any(a => GetAbilityLevel(a) == 2))
        {
            AddAbilityByLevel(2);
        }
        if (level == 10 && !_character.CharacterAbilities.Any(a => GetAbilityLevel(a) == 3))
        {
            AddAbilityByLevel(3);
        }
    }

    /// <summary>
    /// �������� �� ���������� ����������� � ���������� �� � ������
    /// </summary>
    /// <param name="requiredLevel"></param>
    private void AddAbilityByLevel(int requiredLevel)
    {
        Ability newAbility = _character.AllCharacterAbilities.FirstOrDefault(ability => GetAbilityLevel(ability) == requiredLevel);
        if (newAbility != null && !_character.CharacterAbilities.Contains(newAbility))
        {
            _character.AddAbilities(newAbility);
        }
    }

    /// <summary>
    ///     ����� ����������� ������� �������, ��������������� ����� ������,
    /// ����������� �� ������ ��������� ��� ������ ��������
    /// ��������, ���� ������� ������� ����� ������� � ������ AllCharacterAbilities, �� ����� ������� ������ + 1
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    private int GetAbilityLevel(Ability ability)
    {
    
        return _character.AllCharacterAbilities.IndexOf(ability) + 1;
    }




}
