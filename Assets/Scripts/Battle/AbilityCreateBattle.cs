using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityCreateBattle : MonoBehaviour
{
    [SerializeField] private AbilityUseUI _baseAbility;  // ������� ������ ������
    [SerializeField] private AbilityUseUI[] _abilityButtons;  // ��������� ������ ������
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
    /// ������� ������ ������ ��������� ������ ������� �� ������ ���� �������
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
    /// �������� ������� ����������� ������ � ������
    /// </summary>
    private void CreateBaseAbility()
    {
        _baseAbility.LoadAbility(_abilitySystem.AbilitiesList[0].Icon, _abilitySystem.AbilitiesList[0]);
    }

    /// <summary>
    /// �������� ������ ��� ������ � UI
    /// </summary>
    private void CreateButtonsAbility()
    {
        int maxAbilitiesToShow = 3; // ������������ ���������� ������ ��� �����������

        for (int i = 1; i < _characterCharacteristics.CharacterAbilities.Count && i <= maxAbilitiesToShow; i++)
        {
            _abilityButtons[i - 1].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// �������� ������ ������ ������� ����� �������
    /// </summary>
    /// <param name="isLevelUp"></param>
    private void IsLevelUp(bool isLevelUp)
    {
        if (isLevelUp == true)
        {
            LevelUP++;
            CheckCountLevel();
            foreach (var buttonAbility in _abilityButtons)  // �������� ���� �� ������� �� ��������� ������ ���� ����� ���� ������� ������ ������
            {
                buttonAbility.AddAbilityUIChooseUI(_abilitiesList);
            }

        }
    }

    /// <summary>
    /// �������� ���������� ������� � ��������� �������������� �� ������
    /// </summary>
    private void RemoveLevelUP()
    {
        LevelUP--;
        CheckCountLevel();
    }

    /// <summary>
    ///������� �� ���������� ��� � ��������� ���������� �� ����������� �� ������. ���� ������ 0 �� ����� ��� �������
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
