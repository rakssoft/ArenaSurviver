using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Gear/Chest")]
public class ChestGearSO : Gear
{
     private float Armor;
    public ChestGearSO(GearData gearData)
    {
        Name = gearData.gearName;
        Armor = gearData.armor;
        Value = Armor;
        EquipmentType = GearStyle.chest;
        Sprite = Resources.Load<Sprite>("ChestGearSprite"); // пример, как загрузить спрайт
    }

    public override void Equip(PlayerData playerData, PlayerCharacteristics character)
    {

        playerData.AddGear(new GearData(GearStyle.chest, Name, Value, Sprite.name));
        playerData.IncreaseMaxHealth(Value);
        character.SetPlayerData(playerData);
    }
}
