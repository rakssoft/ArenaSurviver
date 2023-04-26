using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct GearStyle
{
    [SerializeField] private Gear.GearStyle _equipmentType;
    [SerializeField] private Transform _slotTransform;

    public Gear.GearStyle EquipmentType => _equipmentType;
    public Transform SlotTransform => _slotTransform;
}
public class GearInventory : MonoBehaviour
{
    [SerializeField] private Transform[] _slotsTransforms;
    [SerializeField] private Gear[] _gearsEquipOn;  // вся одежда 
    [SerializeField] private GameObject _gearUIPrefab;
    [SerializeField] private GearStyle[] _slotsTransform;
    [SerializeField] private ShowCharacterMenuUI _showCharacterMenuUI;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private List<GameObject> _equippedGears = new List<GameObject>();
    [SerializeField] private InventoryBlock _inventoryBlock;

    [SerializeField] private EquipmentSlot[] _equipmentSlots;

    private Dictionary<GearData, Gear> _gearInstances = new Dictionary<GearData, Gear>();

    public List<GearData> test;



    private void OnEnable()
    {
        EventManager.EquipGearEvent += Equip;
        EventManager.UnEquipGearEvent += UnEquip;
    }
    private void OnDisable()
    {
        EventManager.EquipGearEvent -= Equip;
        EventManager.UnEquipGearEvent -= UnEquip;
    }
    public void OpenInventory()
    {
        _inventoryBlock.ShowAllEquips();
        ShowDataCharacter();
        _playerData = _showCharacterMenuUI.GetPlayerData();
    }

    public void ShowDataCharacter()
    {
        _playerData = _showCharacterMenuUI.GetPlayerData();
    }


    private void UnEquip(Gear gear)
    {
        _inventoryBlock.AddGearBlock(gear );
        gear.UnEquip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
        _playerData.Save();
        _playerDataManager.Save();
        EquipAllGear();
    }

    private void Equip(Gear gear)
    {
        var gearType = gear.EquipmentType;
        bool gearEquipped = HasGearEquipped(gearType);
        bool freeSlot = HasFreeSlot(gear);
    //    Debug.Log($"Gear equipped: {gearEquipped}, Free slot available: {freeSlot}");
        
        if (gearEquipped && !freeSlot)
        {
       //     Debug.Log($"No free slot available to equip {gear.name}.");
            UnEquip(gear);
        }

        gear.Equip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
        _inventoryBlock.EquipGear(gear);
        _playerData.Save();
        _playerDataManager.Save();
        EquipAllGear();
        PrintSlotsInfo();
    }

    private bool HasFreeSlot(Gear gear)
    {
        var gearType = gear.EquipmentType;
        Debug.Log(gearType);
        foreach (var slot in _slotsTransforms)
        {
            var slotGear = slot.GetComponentInChildren<GearUI>();
            if (slotGear != null && slotGear.EquipmentType == gearType)
            {
                // Слот уже занят нужным типом предмета
             //   Debug.Log("Slot " + slot.name + " is occupied by " + slotGear.name);
                return false;
            }
            else if (slotGear == null)
            {
                // Свободный слот
           //     Debug.Log("Slot " + slot.name + " is free");
                return true;
            }
        }
        return false;
    }

    private bool GetFreeSlot(Gear gear)
    {
        foreach (var item in _equipmentSlots)
        {
            if(item._equipmentType == gear.EquipmentType)
            {
              if(item.IsFreeSlots() == true)
                {
                    return true;
                }
            }
        return false;
    }

    private bool HasGearEquipped(Gear.GearStyle gearType)
    {
        foreach (var slot in _gearsEquipOn)
        {
            if (slot.EquipmentType == gearType)
            {
                return true;
            }
        }
        return false;
    }

    private void PrintSlotsInfo()
    {
        foreach (var slotTransform in _slotsTransforms)
        {
            var slotGear = slotTransform.GetComponentInChildren<GearUI>();

            if (slotGear != null)
            {
                Debug.Log($"Slot {slotTransform.name} is not free");
            }
            else
            {
                Debug.Log($"Slot {slotTransform.name} is free");
            }
        }
    }


    /*    private void Equip(Gear gear)
        {
            gear.Equip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
            _inventoryBlock.EquipGear(gear);
            _playerData.Save();
            _playerDataManager.Save();
            EquipAllGear();
        }*/



    /// <summary>
    ///  при ивыходе снимаем визуально всю ждежду чтобы она не отображалась
    /// </summary>
    public void CLoseInventory()
    {
        _inventoryBlock.CloseInventory();
        foreach (GameObject gear in _equippedGears)
        {
            Destroy(gear);
        }
        _equippedGears.Clear();

    }


    /// <summary>
    /// При открытии инвенторя надевай всю одежду которая есть уже у персонажа.
    /// </summary>
    public void EquipAllGear()
    {
        // Удаляем все созданные экземпляры
        foreach (GameObject gearUI in _equippedGears)
        {
            Destroy(gearUI);
        }
        _equippedGears.Clear();

        // Создаем экземпляры заново для каждого надеваемого предмета
        List<GearData> tempList = new List<GearData>(_playerData.gearList);
        foreach (GearData gearData in tempList)
        {
            Gear gear = CreateGearInstance(gearData);
            if (gear != null)
            {
                for (int i = 0; i < _gearsEquipOn.Length; i++)
                {
                    if (_gearsEquipOn[i].EquipmentType == gear.EquipmentType)
                    {
                        for (int j = 0; j < _slotsTransform.Length; j++)
                        {
                            if (_slotsTransform[j].EquipmentType == gear.EquipmentType)
                            {
                                gear.SpriteCharackteristic = _gearsEquipOn[i].SpriteCharackteristic;
                                GameObject gearUI = Instantiate(_gearUIPrefab, _slotsTransform[j].SlotTransform);  // создаем префаб
                                gearUI.GetComponent<GearUI>().ShowGear(_gearsEquipOn[i]);
                                gearUI.GetComponent<GearUI>().IsEquipped = true;
                                _equippedGears.Add(gearUI);
                                ShowDataCharacter();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }







    /// <summary>
    /// Функция для определения какой скрипт необоходимо вернуть для одежды чтобы понимать куда надеть
    /// </summary>
    /// <param name="gearData"></param>
    /// <returns></returns>
    private Gear CreateGearInstance(GearData gearData)
    {
        switch (gearData.equipmentType)
        {
            case Gear.GearStyle.head:
                return new HeadGearSO(gearData);
            case Gear.GearStyle.chest:
                return new ChestGearSO(gearData);
            case Gear.GearStyle.foot:
                return new FootGearSO(gearData);
  /*          case Gear.GearStyle.beads:
                return new BeadsGear(gearData);
            case Gear.GearStyle.amulet:
                return new AmuletGear(gearData);
            case Gear.GearStyle.belt:
                return new BeltGear(gearData);*/
            default:
           //     Debug.LogError($"Unsupported gear type {gearData.equipmentType}");
                return null;
        }
    }



}