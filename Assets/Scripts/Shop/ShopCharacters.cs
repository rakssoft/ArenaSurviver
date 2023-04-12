using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCharacters : MonoBehaviour
{
    [SerializeField] private PlayerCharacteristics[] _playerCharacteristics;
    [SerializeField] private List<PlayerCharacteristics> _listShopCharacters = new List<PlayerCharacteristics>();
    [SerializeField] private ShowCharacterMenuUI _showCharacter;
    [SerializeField] private Transform _shopTransform;
    [SerializeField] private CharacterShopUI _prefabCharacterShopUI;

    private bool _unLocked;


    public void Awake()
    {
        OpenShopCharacters();
    }
    public void OpenShopCharacters()
    {
        _listShopCharacters.Clear();
        foreach (var character in _playerCharacteristics)
        {
            if(IsUnlookCharacter(character) == true)
            {
                _showCharacter.AddCharactersInChoose(character);
            }
            else
            {
                _listShopCharacters.Add(character);
            }
        }
        ShowCharacter();

    }

    private void ShowCharacter()
    {
        for (int i = 0; i < _listShopCharacters.Count; i++)
        {
            CharacterShopUI characterShopUI = Instantiate(_prefabCharacterShopUI, _shopTransform.position, _shopTransform.rotation, _shopTransform);
            characterShopUI.ShowCharacter(_listShopCharacters[i]);
        }
    }

    private bool IsUnlookCharacter(PlayerCharacteristics character)
    {
        if (character.Unlocked)
        {
            _unLocked = true;
        }
        else
        {
            _unLocked = false;
        }
        return _unLocked;
    }



}
