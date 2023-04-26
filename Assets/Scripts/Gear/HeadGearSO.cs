using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Gear/Head")]
public class HeadGearSO : Gear
{
    private float Armor;
    public HeadGearSO(GearData gearData)
    {
        Name = gearData.name;
        Armor = gearData.value;
        Value = Armor;
        EquipmentType = GearStyle.head;
        Sprite = Resources.Load<Sprite>("HeadGearSprite"); // пример, как загрузить спрайт

    }

    public override void Equip(PlayerData playerData, PlayerCharacteristics character)
    {

        playerData.AddGear(new GearData(Gear.GearStyle.head, Name, Value, Sprite.name, InstanceID));
        playerData.IncreaseMaxHealth(Value);
        character.SetPlayerData(playerData);
    }

    public override void UnEquip(PlayerData playerData, PlayerCharacteristics character)
    {
        GearData gearToRemove = null;
        foreach (GearData gear in playerData.gearList)
        {
            if (gear.name == Name)
            {
                gearToRemove = gear;
                break;
            }
        }

        if (gearToRemove != null)
        {
            playerData.RemoveGear(gearToRemove);
            playerData.IncreaseMaxHealth(Value * -1);
            character.SetPlayerData(playerData);
     //       Debug.Log("Gear unequipped: " + gearToRemove.name);
        }
        else
        {
        //    Debug.Log("Gear not found: " + Name);
        }
    }


}



