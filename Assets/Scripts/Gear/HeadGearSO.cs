using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Gear/Head")]
public class HeadGearSO : Gear
{
    private float Armor;
    public HeadGearSO(GearData gearData)
    {
        Name = gearData.gearName;
        Armor = gearData.armor;
        Value = Armor;
        EquipmentType = GearStyle.head;
        Sprite = Resources.Load<Sprite>("HeadGearSprite"); // пример, как загрузить спрайт

    }

    public override void Equip(PlayerData playerData, PlayerCharacteristics character)
    {
        
        playerData.AddGear(new GearData(GearStyle.head, Name, Value, Sprite.name));
        playerData.IncreaseMaxHealth(Value);
        character.SetPlayerData(playerData);
    }

}



