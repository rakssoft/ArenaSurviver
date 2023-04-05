using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GearShopUI : MonoBehaviour
{
    private Gear _gear;
    [SerializeField] private GearUI _gearUI;
    [SerializeField] private TextMeshProUGUI _nameGearShop;
    [SerializeField] private TextMeshProUGUI _priceGearShop;

    // Сделать чтобы вызов был не через аваке а старт.  ЧТобы передать в гир юай


    public void ValueGear(Gear value)
    {
        _gear = value;
        _gearUI.ShowGear(_gear);
        _nameGearShop.text = _gear.Name;
        _priceGearShop.text = _gear.Price.ToString("F0");
    }

    

   
}
