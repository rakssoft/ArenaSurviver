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
            // ������������ ������ �������� �������� �������
            return false;
        }

        return true;
    }
}
