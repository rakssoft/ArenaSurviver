using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public Gear.GearStyle _equipmentType;
    public Gear GearSlot;


    /// <summary>
    /// есть ли дочерний обьект   если есть вернуть его Gear
    /// </summary>
    /// <returns></returns>
    public Gear GetGearSlot()
    {
        GearUI gearUI = gameObject.transform.GetComponentInChildren<GearUI>();
        if (gearUI == null)
        {
            return null;
        }

        GearSlot = gearUI.GearSlot;
        if (GearSlot == null)
        {
            return null;
        }
        return GearSlot;
    }


    /// <summary>
    /// свободен ли слот правда свободен, ложь  не свободен.
    /// </summary>
    /// <returns></returns>
    public bool IsFreeSlots()
    {
        if (gameObject.transform.childCount > 0)
        {
            // Родительский объект содержит дочерние объекты
            GetGearSlot();
            return false;
        }
        return true;
    }

}
