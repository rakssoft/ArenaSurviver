using UnityEngine;
using UnityEngine.UI;

public class AddAbilityButton : MonoBehaviour
{
    public CharacterCharacteristics character; // ������ �� ��������� CharacterAbility
    public Ability abilityToAdd; // Ability, ������� ����� ��������

    private void Start()
    {
        // ������� ��������� Button �� ���� �� ������� �������
        Button button = GetComponent<Button>();

        // ����������� ���������� ������� ������� ������
        button.onClick.AddListener(AddAbility);
    }

    private void AddAbility()
    {
        // ���������, ��� ���� ������ �� CharacterAbility � Ability ��� ����������
        if (character != null && abilityToAdd != null)
        {
            // ���������, ��� ������ ��� �� ��������� � ������
            if (!character.CharacterAbilities.Contains(abilityToAdd))
            {
                // ��������� Ability � CharacterAbility
                character.AddAbilities(abilityToAdd);               
             
            }
            else
            {
                Debug.Log("Ability already exists in CharacterAbility.");
            }
        }
    }

}
