[System.Serializable]
public class GearData
{
    public Gear.GearStyle gearType;
    public string gearName;
    public float armor;
    // ������ ��������������, ���� �����

    public GearData(Gear.GearStyle type, string name, float armor)
    {
        this.gearType = type;
        this.gearName = name;
        this.armor = armor;
    }
}

