using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GearShopUI : MonoBehaviour
{
    public Gear _gear;
    [SerializeField] private TextMeshProUGUI _nameGearShop;
    [SerializeField] private TextMeshProUGUI _priceGearShop;
    [SerializeField] private Image _spriteGear;
    [SerializeField] private Image _spriteCharacteristic;
    [SerializeField] private TextMeshProUGUI _valueCharacteristic;
    


    public void ValueGear(Gear value)
    {
        _gear = value;
        _nameGearShop.text = _gear.Name;
        _priceGearShop.text = _gear.Price.ToString("F0");
        _spriteGear.sprite = _gear.Sprite;
        _spriteCharacteristic.sprite = _gear.SpriteCharackteristic;
        _valueCharacteristic.text = _gear.Value.ToString();
    }

    public void BuyEquip()
    {
        EventManager.PurchaseCompleted?.Invoke(_gear);
        Destroy(gameObject);
    }




}
