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
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private CharacterDataManager _characterDataManager;
    [SerializeField] private List<GameObject> _equippedGears = new List<GameObject>();
    [SerializeField] private InventoryBlock _inventoryBlock;
    [SerializeField] private EquipmentSlot[] _equipmentSlots;


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
        _characterData = _showCharacterMenuUI.GetPlayerData();
    }

    public void ShowDataCharacter()
    {
        _characterData = _showCharacterMenuUI.GetPlayerData();
    }

    private void UnEquip(Gear gear)
    {
        _inventoryBlock.AddGearBlock(gear);
        gear.UnEquip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
        _characterData.Save();
        _characterDataManager.Save();
        EquipAllGear();
    }

    private void Equip(Gear gear)
    {
        // bool freeSlot = GetIsFreeSlot(gear);
        Gear freeSlot = GetFreeSlot(gear);
     //   Debug.Log(freeSlot);
        if (freeSlot != null)
        {               
            UnEquip(freeSlot);
            gear.Equip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
            _inventoryBlock.EquipGear(gear);
            _characterData.Save();
            _characterDataManager.Save();
            EquipAllGear();

        }
        else if (freeSlot == null)
        {
            gear.Equip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
            _inventoryBlock.EquipGear(gear);
            _characterData.Save();
            _characterDataManager.Save();
            EquipAllGear();
        }
    }

    private Gear GetFreeSlot(Gear gear)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (_equipmentSlots[i]._equipmentType == gear.EquipmentType && _equipmentSlots[i].IsFreeSlots() == false)
            {             
                //подходит по типу но слот не свободен а чем то зан€т.  
                //возвращает то чем зан€т слот.
                return _equipmentSlots[i].GearSlot;
            }
        }

        return null;
    }


    /// <summary>
    /// возврати правду или ложь если свободен ли слот. 
    /// правда свободен ложь не свободен
    /// </summary>
    /// <param name="gear"></param>
    /// <returns></returns>
    private bool  GetIsFreeSlot(Gear gear)
    {
        for (int i = 0; i < _equippedGears.Count; i++)
        {
            if (_equippedGears[i].GetComponent<GearUI>().EquipmentType == gear.EquipmentType)
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
        // ”дал€ем все созданные экземпл€ры
        foreach (GameObject gearUI in _equippedGears)
        {
            Destroy(gearUI);
        }
        _equippedGears.Clear();

        // —оздаем экземпл€ры заново дл€ каждого надеваемого предмета
        List<GearData> tempList = new List<GearData>(_characterData.gearList);
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
            case Gear.GearStyle.weapon:
                return new WeaponGearSO(gearData);
            /*          case Gear.GearStyle.beads:
                          return new BeadsGear(gearData);

                      case Gear.GearStyle.belt:
                          return new BeltGear(gearData);*/
            default:
           //     Debug.LogError($"Unsupported gear type {gearData.equipmentType}");
                return null;
        }
    }



}