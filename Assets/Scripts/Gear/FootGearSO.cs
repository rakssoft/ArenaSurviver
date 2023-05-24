using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Gear/Foot")]
public class FootGearSO : Gear
{
    private float Speed;
    public FootGearSO(GearData gearData)
    {
        Name = gearData.name;
        Speed = gearData.value;
        Value = Speed;
        EquipmentType = GearStyle.foot;
        Sprite = Resources.Load<Sprite>("FootGearSprite"); // пример, как загрузить спрайт
    }

    public override void Equip(CharacterData characterData, CharacterCharacteristics character)
    {

        characterData.AddGear(new GearData(Gear.GearStyle.foot, Name, Value, Sprite.name, InstanceID));
        characterData.IncreaseSpeed(Value);
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
            characterData.IncreaseSpeed(Value * -1);
            character.SetPlayerData(characterData);
            Debug.Log("Gear unequipped: " + gearToRemove.name);
        }
        else
        {
            Debug.Log("Gear not found: " + Name);
        }
    }
}

