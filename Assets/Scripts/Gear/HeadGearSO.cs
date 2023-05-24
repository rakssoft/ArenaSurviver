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

    public override void Equip(CharacterData characterData, CharacterCharacteristics character)
    {

        characterData.AddGear(new GearData(Gear.GearStyle.head, Name, Value, Sprite.name, InstanceID));
        characterData.IncreaseMaxHealth(Value);
        character.SetPlayerData(characterData);
    }

    public override void UnEquip(CharacterData characterData, CharacterCharacteristics character)
    {
        GearData gearToRemove = null;
        foreach (GearData gear in characterData.gearList)
        {
            if (gear.name == Name)
            {
                gearToRemove = gear;
                break;
            }
        }

        if (gearToRemove != null)
        {
            characterData.RemoveGear(gearToRemove);
            characterData.IncreaseMaxHealth(Value * -1);
            character.SetPlayerData(characterData);
     //       Debug.Log("Gear unequipped: " + gearToRemove.name);
        }
        else
        {
        //    Debug.Log("Gear not found: " + Name);
        }
    }


}



