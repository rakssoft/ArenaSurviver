using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityCreateBattle : MonoBehaviour
{
    [SerializeField] private AbilityUseUI _baseAbility;  // базовая кнопка абилки
    [SerializeField] private AbilityUseUI[] _abilityButtons;  // остальные кнопки абилок
    [SerializeField] private int LevelUP;
    [SerializeField] private GameObject[] _levelUpAbilityButtonsUI;
    [SerializeField] private List<Ability> _abilitiesList = new List<Ability>();

    [SerializeField] private CharacterCharacteristics _characterCharacteristics;
    [SerializeField] private AbilitySystem _abilitySystem;
    private void OnEnable()
    {
        EventManager.LevelUp += IsLevelUp;
        EventManager.CharacterAbilityLevelUp += RemoveLevelUP;
        EventManager.AddAbility += RemoveListAbility;
    }



    private void OnDisable()
    {
        EventManager.LevelUp -= IsLevelUp;
        EventManager.CharacterAbilityLevelUp -= RemoveLevelUP;
        EventManager.AddAbility -= RemoveListAbility;

    }

    private void Start()
    {
        LevelUP = 0;
    }

    public void ShowUIButtonsAbility(AbilitySystem abilitySystem, CharacterCharacteristics characterCharacteristics)
    {
        _characterCharacteristics = characterCharacteristics;
        _abilitySystem = abilitySystem;
        CreateBaseAbility();
        CreateButtonsAbility();
        CreateListAbility();
    }

    /// <summary>
    /// создает список абилок пропуская первый элемент он должен быть базовым
    /// </summary>
    private void CreateListAbility()
    {
        _abilitiesList = _characterCharacteristics.CharacterAbilities.Skip(1).ToList();
    }
    private void RemoveListAbility(Ability ability)
    {
        _abilitiesList.Remove(ability);
    }

    /// <summary>
    /// создание базовой способности первой в списке
    /// </summary>
    private void CreateBaseAbility()
    {
        _baseAbility.LoadAbility(_abilitySystem.AbilitiesList[0].Icon, _abilitySystem.AbilitiesList[0]);
    }

    /// <summary>
    /// создание кнопок для абилок в UI
    /// </summary>
    private void CreateButtonsAbility()
    {
        int maxAbilitiesToShow = 3; // Максимальное количество абилок для отображения

        for (int i = 1; i < _characterCharacteristics.CharacterAbilities.Count && i <= maxAbilitiesToShow; i++)
        {
            _abilityButtons[i - 1].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// передаем список абилок которые можно открыть
    /// </summary>
    /// <param name="isLevelUp"></param>
    private void IsLevelUp(bool isLevelUp)
    {
        if (isLevelUp == true)
        {
            LevelUP++;
            CheckCountLevel();
            foreach (var buttonAbility in _abilityButtons)  // проверка если не открыта то открываем делаем один метод если открыта делаем другой
            {
                buttonAbility.AddAbilityUIChooseUI(_abilitiesList);
            }

        }
    }

    /// <summary>
    /// отнимаем полученный уровень у персонажа использованный на абилку
    /// </summary>
    private void RemoveLevelUP()
    {
        LevelUP--;
        CheckCountLevel();
    }

    /// <summary>
    ///прверка на количество лвл у персонажа свободного не потраченого на абилки. Если больше 0 то можно еще тратить
    /// </summary>
    private void CheckCountLevel()
    {
        if (LevelUP > 0)
        {
            foreach (var item in _levelUpAbilityButtonsUI)
            {
                item.SetActive(true);
            }
        }
        else
        {
            foreach (var item in _levelUpAbilityButtonsUI)
            {
                item.SetActive(false);
            }
        }
    }

}
