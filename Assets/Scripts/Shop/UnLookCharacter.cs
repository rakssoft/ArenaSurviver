using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLookCharacter : MonoBehaviour
{
    public List<PlayerCharacteristics> characters;

    public void Start()
    {
        // ���������, ����� ��������� ��� �������
        foreach (PlayerCharacteristics character in characters)
        {
            if (character.Unlocked)
            {
                // ������� ��� ��������� ���������
                Debug.Log(character.Name + " is unlocked");
            }
            else
            {
                 �������� � ��������� ������� ������������������ ���������
                if (character.Purchase(100))
                {
                    character.Unlock();
                    Debug.Log("Purchased and unlocked " + character.Name);
                    break;
                }
                else
                {
                    Debug.Log("Not enough coins to purchase " + character.Name);
                }
            }
        }
    }
}
