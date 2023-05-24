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
    [SerializeField] private CharacterDataManager _characterDataManager;
    private CharacterCharacteristics _characterCharacteristics;
    
    public void ShowCharacter(CharacterCharacteristics characterCharacteristics)
    {
        _characterCharacteristics = characterCharacteristics;
        _name.text = _characterCharacteristics.Name.ToUpper().ToString();
        _avatar.sprite = _characterCharacteristics.Avatar;
        _cost.text = _characterCharacteristics.Cost.ToString("F0");
    }


    public void Purchase()
    {
        float coinCount = Wallet.Instance.coins;

        if (coinCount >= _characterCharacteristics.Cost)
        {
            Wallet.Instance.RemoveCoins(_characterCharacteristics.Cost);
            _characterCharacteristics.Unlock();
            // —охран€ем данные после разблокировки персонажа

            EventManager.AddCharacterCharacteristics?.Invoke(_characterCharacteristics);
            EventManager.PurchaseIsCompleted?.Invoke();
            CharacterDataManager.Instance.Save();
            Destroy(gameObject);
        }
    }





}
