using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public Gear.GearStyle _equipmentType;



    public bool IsFreeSlots()
    {
        if (gameObject.transform.childCount > 0)
        {
            // Родительский объект содержит дочерние объекты
            return false;
        }

        return true;
    }
}
