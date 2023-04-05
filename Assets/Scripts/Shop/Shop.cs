using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Gear> _gears = new List<Gear>();
    [SerializeField] private List<GearShopUI> _gearListShop = new List<GearShopUI>();
    [SerializeField] private Transform _shopTransform;
    [SerializeField] private GearShopUI _prefabGearShopUI;



    public void FillShop()
    {
        _gearListShop.Clear();
        for (int i = 0; i < RandomValueFillShop(); i++)
        {
            GearShopUI gearShopUI = Instantiate(_prefabGearShopUI, _shopTransform.position, _shopTransform.rotation, _shopTransform);
            gearShopUI.ValueGear(_gears[RandomValueGear()]);
            _gearListShop.Add(gearShopUI);
        }
    }


    private int RandomValueFillShop()
    {
        int ValueFillShop = Random.RandomRange(0, 5);
        return ValueFillShop;
    }

    private int RandomValueGear()
    {
        int ValueValueGear = Random.RandomRange(0, _gears.Count);
        return ValueValueGear;
    }


}
