using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _avatar;
    [SerializeField] private TextMeshProUGUI _cost;
    private PlayerCharacteristics _playerCharacteristics;
    
    public void ShowCharacter(PlayerCharacteristics playerCharacteristics)
    {
        _playerCharacteristics = playerCharacteristics;
        _name.text = _playerCharacteristics.Name.ToUpper().ToString();
        _avatar.sprite = _playerCharacteristics.Avatar;
        _cost.text = _playerCharacteristics.Cost.ToString("F0");
    }


    public void Purchase()
    {
        _playerCharacteristics.Unlock();
        EventManager.AddPlayerCharacteristics?.Invoke(_playerCharacteristics);
        Destroy(gameObject);
    }



}
