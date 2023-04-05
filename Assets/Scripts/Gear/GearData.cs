
using UnityEngine;

[System.Serializable]
public class GearData
{
    public Gear.GearStyle gearType;
    public string gearName;
    public float armor;
    public string spriteName;

    public GearData(Gear.GearStyle type, string name, float armor, string spriteName)
    {
        this.gearType = type;
        this.gearName = name;
        this.armor = armor;
        this.spriteName = spriteName;
    }
}



