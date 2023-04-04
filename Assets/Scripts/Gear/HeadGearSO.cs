using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Gear/Head")]
public class HeadGearSO : Gear
{
    
    private float Armor;


    public override void Equip(PlayerData playerData, PlayerCharacteristics character)
    {
        Armor = Value;
        GearData gearData = new GearData(GearStyle.head, name, Armor);
        playerData.AddGear(gearData);


        playerData.IncreaseMaxHealth(Armor);
        character.SetPlayerData(playerData);


    }



}
