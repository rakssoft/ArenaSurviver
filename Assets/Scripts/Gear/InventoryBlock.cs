using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBlock : MonoBehaviour
{
    [SerializeField] private List<Gear> _allEquips = new List<Gear>();
    [SerializeField] private List<GameObject> _prefabsUIGO = new List<GameObject>();
    [SerializeField] private GearUI _prefabUI;
    [SerializeField] private Transform _equipSpawn;
    [SerializeField] private InventoryData _inventoryData;


    public void ShowAllEquips()
    {
        
        _inventoryData.Load();
        _allEquips.Clear();
        _allEquips.AddRange(_inventoryData.gearList); // копирование всех элементов из _inventoryData.gearList в _allEquips
        while (_allEquips.Count > 0)
        {
            GearUI prefabGear = Instantiate(_prefabUI, _equipSpawn.position, _equipSpawn.rotation, _equipSpawn);
            _prefabsUIGO.Add(prefabGear.gameObject);
            prefabGear.ShowGear(_allEquips[0]);
            prefabGear.IsEquipped = false;
            _allEquips.RemoveAt(0);
        }
    }

    public void CloseInventory()
    {
        foreach (GameObject gearUI in _prefabsUIGO)
        {
            Destroy(gearUI.gameObject);
        }
        _prefabsUIGO.Clear();
        _inventoryData.Save();
    }


    public void EquipGear(Gear equip)
    {
        _inventoryData.gearList.Remove(equip);
        CloseInventory();
        ShowAllEquips();
    }

    public void AddGearBlock(Gear gear)
    {
        _inventoryData.gearList.Add(gear); // Добавить GearData в список GearData в объекте _inventoryData
        CloseInventory();
        ShowAllEquips();
    }







}
