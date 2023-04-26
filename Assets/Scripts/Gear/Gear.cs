using UnityEngine;
using System;

public abstract class Gear : ScriptableObject
{
    public string Name;
    public int TierGear;
    public Sprite Sprite;
    public Sprite SpriteCharackteristic;
    public float Value;
    public GearStyle EquipmentType;
    public float Price;
    public GearData GearData { get; set; }
    public long InstanceID { get; set; } // уникальный идентификатор экземпляра предмета
    public enum GearStyle
    {
        head = 0,
        chest = 1,
        foot = 2,
        beads = 3,
        amulet = 4,
        belt = 5,
        None = 6
    }

    public abstract void Equip(PlayerData player, PlayerCharacteristics character);
    public abstract void UnEquip(PlayerData player, PlayerCharacteristics character);

    private void OnEnable()
    {
        InstanceID = DateTime.Now.GetHashCode(); // устанавливаем значение InstanceID при создании экземпляра
    }


}
