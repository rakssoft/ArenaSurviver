using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Gear/Chest")]
public class ChestGearSO : Gear
{
     private float Armor;
    public ChestGearSO(GearData gearData)
    {
        Name = gearData.name;
        Armor = gearData.value;
        Value = Armor;
        EquipmentType = GearStyle.chest;
        Sprite = Resources.Load<Sprite>("ChestGearSprite"); // пример, как загрузить спрайт
    }

    public override void Equip(CharacterData characterData, CharacterCharacteristics character)
    {

        characterData.AddGear(new GearData(Gear.GearStyle.chest, Name, Value, Sprite.name, InstanceID));
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
      //      Debug.Log("Gear unequipped: " + gearToRemove.name);
        }
        else
        {
            Debug.Log("Gear not found: " + Name);
        }
    }
}
