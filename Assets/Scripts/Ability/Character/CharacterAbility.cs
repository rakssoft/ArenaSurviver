using UnityEngine;
using System.Collections.Generic;
using System.IO;

[CreateAssetMenu(fileName = "CharacterAbility", menuName = "CharacterAbility/Abilitys")]
public class CharacterAbility : ScriptableObject
{
    public List<Ability> Abilities = new List<Ability>();

    // �������������� ������ � ��������, ���� ����������

    public void AddAbility(Ability newAbility)
    {
        Abilities.Add(newAbility);
        
    }

    public void RemoveAbility(Ability ability)
    {
        Abilities.Remove(ability);
    }

    // �������������� ������ � ��������, ���� ����������
}
