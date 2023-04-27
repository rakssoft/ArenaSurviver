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
    [SerializeField] private Gear[] _allGears;  // вс€ одежда 
    [SerializeField] private GameObject _gearUIPrefab;
    [SerializeField] private GearStyle[] _slotsTransform;
    [SerializeField] private ShowCharacterMenuUI _showCharacterMenuUI;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private List<GameObject> _equippedGears = new List<GameObject>();
    [SerializeField] private InventoryBlock _inventoryBlock;


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
        bool freeSlot = GetIsFreeSlot(gear);
        if (freeSlot == false)
        {
            UnEquip(gear);
        }
            gear.Equip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
            _inventoryBlock.EquipGear(gear);
            _playerData.Save();
            _playerDataManager.Save();
            EquipAllGear();
    }

    /// <summary>
    /// возврати правду или ложь если свободен ли слот. 
    /// правда свободен ложь не свободен
    /// </summary>
    /// <param name="gear"></param>
    /// <returns></returns>
    private bool  GetIsFreeSlot(Gear gear)
    {
        foreach (GameObject equipped in _equippedGears)
        {
           if(equipped.GetComponent<GearUI>().EquipmentType == gear.EquipmentType)
            {
                return false;
            } 
        }
        return true;
    }


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
    public void EquipAllGear()
    {
        Debug.Log("1");  гдето здесь надо смотреть почему шлем два раза одеваетс€ 
        // ”дал€ем все созданные экземпл€ры
        foreach (GameObject gearUI in _equippedGears)
        {
            Destroy(gearUI);
        }
        _equippedGears.Clear();

        // —оздаем экземпл€ры заново дл€ каждого надеваемого предмета
        List<GearData> tempList = new List<GearData>(_playerData.gearList);
        foreach (GearData gearData in tempList)
        {
            Gear gear = CreateGearInstance(gearData);
            if (gear != null)
            {
                for (int i = 0; i < _allGears.Length; i++)
                {
                    if (_allGears[i].Name == gear.Name)
                    {
                        for (int j = 0; j < _slotsTransform.Length; j++)
                        {
                            if (_slotsTransform[j].EquipmentType == gear.EquipmentType)
                            {
                                gear.SpriteCharackteristic = _allGears[i].SpriteCharackteristic;
                                GameObject gearUI = Instantiate(_gearUIPrefab, _slotsTransform[j].SlotTransform);  // создаем префаб
                                gearUI.GetComponent<GearUI>().ShowGear(_allGears[i]);
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
    /// ‘ункци€ дл€ определени€ какой скрипт необоходимо вернуть дл€ одежды чтобы понимать куда надеть
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