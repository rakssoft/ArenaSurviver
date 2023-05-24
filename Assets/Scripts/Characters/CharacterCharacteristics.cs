using UnityEngine;
using System.Collections.Generic;
using System.IO;

[CreateAssetMenu(fileName = "CharacterCharacteristics", menuName = "Character/Characteristics")]
public class CharacterCharacteristics : ScriptableObject
{
    [SerializeField] private CharacterData _characterData;

    public float Speed => _characterData.speed;
    public float BaseAttack => _characterData.baseAttack;
    public float MaxHealth => _characterData.health;
    public string Name;
    public GameObject PrefabCharacter;
    public bool Unlocked => _characterData.isUnlocked;
    public Sprite Avatar;
    public float Cost;
    public int Level => _characterData.GetCurrentLevel();
    public float Experience => _characterData.GetCurrentExperience();
    public float ExperienceNeededForLevel => _characterData.GetExperienceNeededForLevel(Level);

    public List<Ability> CharacterAbilities => _characterData.AbilitiesCharacterList; 


    public void SetPlayerData(CharacterData characterData)
    {
        _characterData.speed = characterData.speed;
        _characterData.baseAttack = characterData.baseAttack;
        _characterData.health = characterData.health;
        _characterData.playerName = characterData.playerName;
        _characterData.gearList = characterData.gearList;
        _characterData.isUnlocked = characterData.isUnlocked;
        _characterData.Level = characterData.Level;
        _characterData.Experience = characterData.Experience;
        _characterData.AbilitiesCharacterList = characterData.AbilitiesCharacterList;

        Name = characterData.playerName;
    }
    public void Unlock()
    {
        CharacterData characterData = CharacterDataManager.Instance.GetPlayerData(_characterData.playerName);
        if (characterData != null)
        {
            characterData.UnlockedCharacter(true);
            CharacterDataManager.Instance.Save();
        }
    }
    public float GetBaseDamage()
    {
        return BaseAttack;
    }

    public void AddAbilities(Ability ability)
    {
        CharacterAbilities.Add(ability);
        CharacterDataManager.Instance.Save();
    }


}
