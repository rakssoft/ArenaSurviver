using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Gear/Weapon")]
public class WeaponGearSO : Gear
{
    private float Attack;
    public WeaponGearSO(GearData gearData)
    {
        Name = gearData.name;
        Attack = gearData.value;
        Value = Attack;
        EquipmentType = GearStyle.weapon;
        Sprite = Resources.Load<Sprite>("WeaponGearSprite"); // пример, как загрузить спрайт
    }

    public override void Equip(CharacterData characterData, CharacterCharacteristics character)
    {

        characterData.AddGear(new GearData(Gear.GearStyle.weapon, Name, Value, Sprite.name, InstanceID));
        characterData.IncreaseBaseAttack(Value);
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
            characterData.IncreaseBaseAttack(Value * -1);
            character.SetPlayerData(characterData);
            Debug.Log("Gear unequipped: " + gearToRemove.name);
        }
        else
        {
            Debug.Log("Gear not found: " + Name);
        }
    }
}