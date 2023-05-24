using UnityEngine;
using UnityEngine.UI;

public class AddAbilityButton : MonoBehaviour
{
    public CharacterCharacteristics character; // Ссылка на экземпляр CharacterAbility
    public Ability abilityToAdd; // Ability, который будет добавлен

    private void Start()
    {
        // Находим компонент Button на этой же игровой объекте
        Button button = GetComponent<Button>();

        // Привязываем обработчик события нажатия кнопки
        button.onClick.AddListener(AddAbility);
    }

    private void AddAbility()
    {
        // Проверяем, что есть ссылка на CharacterAbility и Ability для добавления
        if (character != null && abilityToAdd != null)
        {
            // Проверяем, что абилка еще не добавлена в список
            if (!character.CharacterAbilities.Contains(abilityToAdd))
            {
                // Добавляем Ability в CharacterAbility
                character.AddAbilities(abilityToAdd);               
             
            }
            else
            {
                Debug.Log("Ability already exists in CharacterAbility.");
            }
        }
    }

}
