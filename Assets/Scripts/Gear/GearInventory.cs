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
    [SerializeField] private Gear[] _gearsEquipOn;  // вс€ одежда 
    [SerializeField] private GameObject _gearUIPrefab;
    [SerializeField] private GearStyle[] _slotsTransform;
    [SerializeField] private ShowCharacterMenuUI _showCharacterMenuUI;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private List<GameObject> _equippedGears = new List<GameObject>();
    [SerializeField] private InventoryBlock _inventoryBlock;

    public List<GearData> test;



    private void OnEnable()
    {
        EventManager.EquipGearEvent += ItemPutOn;
    }
    private void OnDisable()
    {
        EventManager.EquipGearEvent -= ItemPutOn;
    }
    public void OpenInventory()
    {
        ShowDataCharacter();
        _inventoryBlock.ShowAllEquips();
        _playerData = _showCharacterMenuUI.GetPlayerData();
    }

    public void ShowDataCharacter()
    {
        _playerData = _showCharacterMenuUI.GetPlayerData();
    }

    /// <summary>
    /// Ќадеть одежду
    /// </summary>
    /// <param name="gear"></param>
    private void ItemPutOn(Gear gear)
    {
        for (int i = 0; i < _gearsEquipOn.Length; i++)
        {  
            if (_gearsEquipOn[i].EquipmentType == gear.EquipmentType)
            {
                for (int j = 0; j < _slotsTransform.Length; j++)
                {
                    if (_slotsTransform[j].EquipmentType == gear.EquipmentType)
                    {
                        gear.Equip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
                        
                        EquipAllGear();
                        _inventoryBlock.EquipGear(gear);
                        _playerData.Save();
                        _playerDataManager.Save();
                        
                   //     _equippedGears.Add(gearUI); // добавл€ем надетый предмет в список
                    }
                }
                разобратьс€ с блоком сделать или разделить на два класа или передавать гер и сразу отображать
            }
        }
    }
    /// <summary>
    ///  при ивыходе снимаем визуально всю ждежду чтобы она не отображалась
    /// </summary>
    public void CLoseInventory()
    {
        _inventoryBlock.CloseInventory();
        // пр ивыходе снимаем визуально всю ждежду чтобы она не отображалась
        foreach (GameObject gear in _equippedGears)
        {
            Destroy(gear);
        }
        _equippedGears.Clear();

    }

    /// <summary>
    /// ѕри открытии инвентор€ надевай всю одежду котора€ есть уже у персонажа.
    /// </summary>
    public void EquipAllGear()
    {       
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
                                gearUI.GetComponent<GearUI>().ShowGear(gear);
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
        switch (gearData.gearType)
        {
            case Gear.GearStyle.head:
                return new HeadGearSO(gearData);
            case Gear.GearStyle.chest:
                return new ChestGearSO(gearData);
            /*            case Gear.GearStyle.foot:
                            return new FootGear(gearData);
                        case Gear.GearStyle.beads:
                            return new BeadsGear(gearData);
                        case Gear.GearStyle.amulet:
                            return new AmuletGear(gearData);
                        case Gear.GearStyle.belt:
                            return new BeltGear(gearData);*/
            default:
                Debug.LogError($"Unsupported gear type {gearData.gearType}");
                return null;
        }
    }



}