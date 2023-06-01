using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private AbilityUILevel[] _abilitiesUILevel;


    public void ShowCharacterAbiliy(CharacterCharacteristics character)
    {
        for (int i = 0; i < _abilitiesUILevel.Length; i++)
        {
            _abilitiesUILevel[i].HideAbility();
        }


        for (int i = 0; i < character.CharacterAbilities.Count; i++)
        {
            _abilitiesUILevel[i].ShowAbility(character.CharacterAbilities[i]);
        }
    }

}
