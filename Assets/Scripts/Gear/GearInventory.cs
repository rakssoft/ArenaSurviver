using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Gear[] _gearsEquipOn;  // надетая одежда 
    [SerializeField] private GameObject _gearUIPrefab;
    [SerializeField] private GearStyle[] _slotsTransform;
    [SerializeField] private ShowCharacterMenuUI _showCharacterMenuUI;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private List<GameObject> _equippedGears = new List<GameObject>();
    [SerializeField] private InventoryBlock _inventoryBlock;



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
    /// Надеть одежду
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
                        GameObject gearUI = Instantiate(_gearUIPrefab, _slotsTransform[j].SlotTransform);  // создаем префаб
                        gearUI.GetComponent<GearUI>().ShowGear(gear);                      
                        gear.Equip(_showCharacterMenuUI.GetPlayerData(),_showCharacterMenuUI.GetPlayerCharacteristcs());
                        ShowDataCharacter();
                        _inventoryBlock.EquipGear(gear);
                        _playerData.Save();
                        _playerDataManager.Save();
                        _equippedGears.Add(gearUI); // добавляем надетый предмет в список
                    }
                }

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
    /// При открытии инвенторя надевай всю одежду которая есть уже у персонажа.
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
                                GameObject gearUI = Instantiate(_gearUIPrefab, _slotsTransform[j].SlotTransform);  // создаем префаб
                                gearUI.GetComponent<GearUI>().ShowGear(gear);                            
                                _equippedGears.Add(gearUI);
                                
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