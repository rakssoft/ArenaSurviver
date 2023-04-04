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

    // Список всех созданных иконок снаряжения в UI
    public List<GearUI> _gearUIList;

    public void OpenInventory()
    {
        ShowDataCharacter();
        _playerData = _showCharacterMenuUI.GetPlayerData();
        // Создаем список для хранения иконок снаряжения в UI
        _gearUIList = new List<GearUI>();
        // Находим все объекты GearUI на сцене и добавляем их в список
        GearUI[] gearUIs = FindObjectsOfType<GearUI>();
        foreach (GearUI gearUI in gearUIs)
        {
            _gearUIList.Add(gearUI);
            foreach (var gear in _gearUIList)
            {
                gear.EquipGearEvent += ItemPutOn;
            }
        }
    }

    public void ShowDataCharacter()
    {
        _playerData = _showCharacterMenuUI.GetPlayerData();
    }

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
                        Debug.Log(gearUI);
                        gear.Equip(_showCharacterMenuUI.GetPlayerData(), _showCharacterMenuUI.GetPlayerCharacteristcs());
                        ShowDataCharacter();
                    }
                }

            }
        }
    }



}