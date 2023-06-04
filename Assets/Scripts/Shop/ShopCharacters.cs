using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCharacters : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics[] _characterCharacteristics;
    [SerializeField] private List<CharacterCharacteristics> _listShopCharacters = new List<CharacterCharacteristics>();
    [SerializeField] private ShowCharacterMenuUI _showCharacter;
    [SerializeField] private Transform _shopTransform;
    [SerializeField] private CharacterShopUI _prefabCharacterShopUI;
    private List<GameObject> _listCharacters = new List<GameObject>();
    private bool _unLocked;


    private void Start()
    {
        OpenShopCharacters();
        CloseShop();
    }

    public void OpenShopCharacters()
    {
        _listShopCharacters.Clear();
        foreach (var character in _characterCharacteristics)
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
            _listCharacters.Add(characterShopUI.gameObject);
        }
    }

    private bool IsUnlookCharacter(CharacterCharacteristics character)
    {
        if (character.Unlocked == true)
        {
            _unLocked = true;
        }
        else
        {
            _unLocked = false;
        }
        return _unLocked;
    }

    public void CloseShop()
    {
        foreach (var item in _listCharacters)
        {
            Destroy(item);
        }
        _listCharacters.Clear();
    }



}
