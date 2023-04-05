using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBlock : MonoBehaviour
{
    [SerializeField] private List<Gear> _allEquips = new List<Gear>();
    [SerializeField] private List<Gear> _allEquipsBufer = new List<Gear>();
    [SerializeField] private List<GameObject> _prefabsUIGO = new List<GameObject>();
    [SerializeField] private GearUI _prefabUI;
    [SerializeField] private Transform _equipSpawn;
    [SerializeField] private InventoryData _inventoryData = new InventoryData();


    public void ShowAllEquips()
    {
        сделать чтобы при создании префаба передавать данные чтобы юай префаба гира отображался в прошлой версии это был старт. Нужно сделать принудительно.
        _inventoryData.Load();
        _allEquipsBufer.Clear();
        _allEquips = _inventoryData.gearList;
        while (_allEquips.Count > 0)
        {
            GearUI prefabGear = Instantiate(_prefabUI, _equipSpawn.position, _equipSpawn.rotation, _equipSpawn);
            
            _prefabsUIGO.Add(prefabGear.gameObject);            
            prefabGear.ShowGear(_allEquips[0]);
            _allEquipsBufer.Add(_allEquips[0]);
            _allEquips.RemoveAt(0);
        }
    }

    public void CloseInventory()
    {
        _inventoryData.gearList = new List<Gear>(_allEquipsBufer);
        _allEquipsBufer.Clear();
        foreach (GameObject gearUI in _prefabsUIGO)
        {
            Destroy(gearUI.gameObject);
        }
        _prefabsUIGO.Clear();
        _inventoryData.Save();
    }

    public void EquipGear(Gear equip)
    {
        if (_allEquipsBufer.Contains(equip))
        {
            _allEquipsBufer.Remove(equip);
        }
    }

    public void AddGearBlock(Gear gear)
    {    
        _inventoryData.gearList.Add(gear); // Добавить GearData в список GearData в объекте _inventoryData
        _inventoryData.Save(); // Сохранить изменения
    }



    public void RemoveGearBlock(Gear gear)
    {
        _inventoryData.gearList.Remove(gear); // Удалить GearData из списка GearData в объекте _inventoryData
        _inventoryData.Save(); // Сохранить изменения
    }
}
