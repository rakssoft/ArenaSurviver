using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "ArenaSurviver/Gear", order = 0)]
public class Gear : ScriptableObject {

public string Name;
public int TierGear;
    public GearStyle EquipmentType;
public float Health;
public float Damage;
public float Speed;
public float Lucky;

    public enum GearStyle
    {
        head = 0,
        chest = 1,
        foot = 2,
        beads = 3,
        amulet = 4,
        belt = 5
    }
}
