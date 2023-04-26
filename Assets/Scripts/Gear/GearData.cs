using UnityEngine;

[System.Serializable]
public class GearData
{
    public Gear.GearStyle equipmentType;
    public string name;
    public float value;
    public string spriteName;
    public long instanceID;

    public GearData(Gear.GearStyle equipmentType, string name, float value, string spriteName, long instanceID)
    {
        this.equipmentType = equipmentType;
        this.name = name;
        this.value = value;
        this.spriteName = spriteName;
        this.instanceID = instanceID;
    }
}


