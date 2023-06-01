using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Gear> _gears = new List<Gear>();
    [SerializeField] private List<GearShopUI> _gearListShop = new List<GearShopUI>();
    [SerializeField] private Transform _shopTransform;
    [SerializeField] private GearShopUI _prefabGearShopUI;
    [SerializeField] private InventoryBlock _inventoryBlock;
    [SerializeField] private TextMeshProUGUI _textCurrentCoins;

    private void OnEnable()
    {
        EventManager.PurchaseGearCompleted += AddEqupipInInventory;
        EventManager.PurchaseIsCompleted += ShowCurrentCoins;
    }
    private void OnDisable()
    {
        EventManager.PurchaseGearCompleted -= AddEqupipInInventory;
        EventManager.PurchaseIsCompleted -= ShowCurrentCoins;
    }

    private void Start()
    {
        Wallet.Instance.AddCoins(5000);
        ShowCurrentCoins();
       
    }

    public void FillShop()
    {
        for (int i = 0; i < _gearListShop.Count; i++)
        {
            if(_gearListShop[i] != null)
            {
                Destroy(_gearListShop[i].gameObject);
            }
            else
            {
                continue;
            }
        }
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
        //  int ValueFillShop = Random.RandomRange(0, 5);
        int CountGearInShop = 3;
        int ValueFillShop = CountGearInShop;
        return ValueFillShop;
    }

    private int RandomValueGear()
    {
        int ValueValueGear = Random.Range(0, _gears.Count);
        return ValueValueGear;
    }

    private void AddEqupipInInventory(Gear gear)
    {
        ShowCurrentCoins();
        _inventoryBlock.AddGearBlock(gear);
       
    }

    private void ShowCurrentCoins()
    {
        float coinCount = Wallet.Instance.coins;
        _textCurrentCoins.text = coinCount.ToString();

    }


}
