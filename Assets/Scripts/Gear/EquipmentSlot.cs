using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public Gear.GearStyle _equipmentType;
    public Gear GearSlot;


    /// <summary>
    /// ���� �� �������� ������   ���� ���� ������� ��� Gear
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
    /// �������� �� ���� ������ ��������, ����  �� ��������.
    /// </summary>
    /// <returns></returns>
    public bool IsFreeSlots()
    {
        if (gameObject.transform.childCount > 0)
        {
            // ������������ ������ �������� �������� �������
            GetGearSlot();
            return false;
        }
        return true;
    }

}
