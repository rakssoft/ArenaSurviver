using UnityEngine;


public abstract class Gear : ScriptableObject
{
    public string Name;
    public int TierGear;
    public Sprite Sprite;
    public Sprite SpriteCharackteristic;
    public float Value;
    public GearStyle EquipmentType;
    public float Price;
    public enum GearStyle
    {
        head = 0,
        chest = 1,
        foot = 2,
        beads = 3,
        amulet = 4,
        belt = 5
    }
    public abstract void Equip(PlayerData player, PlayerCharacteristics character);

}
