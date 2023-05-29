
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AbilityUseUI : MonoBehaviour
{
    [SerializeField] private Button _buttonUseAbility;
    [SerializeField] private Image _imageAbility;
    [SerializeField] private Ability _currentAbility;
    [SerializeField] private Image _timerCooldown;


    [SerializeField] private TextMeshProUGUI _levelAbilityText;
    [SerializeField] private Transform _parentSkill;
    [SerializeField] private AbilityBattleUI _abilityBattleUIPrefab;
    [SerializeField] private GameObject _levelUPAbility;
    [SerializeField] private GameObject _panelChooseAbility;

    [SerializeField] private bool IsOpenAbility;
    [SerializeField] private bool IsOpenPanel;
    [SerializeField] private AbilityBattleUI _abilityBattle;

    private List<AbilityBattleUI> _abilityUIList = new List<AbilityBattleUI>();
    private void OnEnable()
    {

        EventManager.AddAbility += ChooseAbility;
    }

    private void OnDisable()
    {
        EventManager.AddAbility -= ChooseAbility;
    }
    private void Start()
    {
      
        IsOpenPanel = false;
        if(_panelChooseAbility)  _panelChooseAbility.SetActive(false);
        if (_currentAbility)
        {
            _buttonUseAbility.interactable = true;
            IsOpenAbility = true;
        }
        else
        {
            _buttonUseAbility.interactable = false;
            IsOpenAbility = false;
        }
    
        // Находим компонент Button на этой же игровой объекте
        Button button = _buttonUseAbility;

        // Привязываем обработчик события нажатия кнопки
        button.onClick.AddListener(UseAbility);
    }
    /// <summary>
    /// Получение и открытия абилки.
    /// </summary>
    /// <param name="ability"></param>
    public void ChooseAbility(Ability ability)
    {
        if(_currentAbility == null)
        {
            if (IsOpenAbility == false)
            {
                if (IsOpenPanel == true)
                {
                   
                    _currentAbility = ability;
                    _currentAbility.LevelUp();
                    _buttonUseAbility.interactable = true;
                    LoadAbility(ability.Icon, ability);
                    IsOpenAbility = true;
                    EventManager.CharacterAbilityLevelUp?.Invoke();
                }
            }
        }
        ClosePanel();

    }

    /// <summary>
    /// использовать абилку
    /// </summary>
    private void UseAbility()
    {
        EventManager.UseAbality?.Invoke(_currentAbility);
    }

    /// <summary>
    /// изначальная загрузка абилки
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="ability"></param>
    public void LoadAbility(Sprite icon, Ability ability)
    {
        _imageAbility.sprite = icon;
        _currentAbility = ability;
        ShowAiblityCurrentState();
    }



    /// <summary>
    /// при открытии панели абилок создать весь список
    /// </summary>
    /// <param name="characterAbility"></param>
    public void AddAbilityUIChooseUI(List<Ability> characterAbility)
    {
        for (int i = 0; i < characterAbility.Count; i++)
        {
            Ability ability = characterAbility[i];

            AbilityBattleUI abilityUI = Instantiate(_abilityBattleUIPrefab, _parentSkill);
            abilityUI.ShowAbilityUI(ability.Icon, ability);

            _abilityUIList.Add(abilityUI);
        }
    }
    /// <summary>
    /// удаление созданных абилок в панелях при закрытии
    /// </summary>
    public void RemoveAbilityUIChooseUI()
    {
        foreach (AbilityBattleUI abilityUI in _abilityUIList)
        {
            Destroy(abilityUI.gameObject);
        }
        _abilityUIList.Clear();
    }

    /// <summary>
    /// открытие панели выбора абилки. Если уже выбрана то только повышение уровня.
    /// </summary>
    public void OpenPanel()
    {
        if (IsOpenAbility == false)
        {
            if (_panelChooseAbility)
            {
                IsOpenPanel = true;
                _panelChooseAbility.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
        {
            _currentAbility.LevelUp();
            ShowAiblityCurrentState();
            EventManager.CharacterAbilityLevelUp?.Invoke();
            ClosePanel();
        }    
    }

    /// <summary>
    /// показывает текущий уровень абилки
    /// </summary>
    private void ShowAiblityCurrentState()
    {
        if (_levelAbilityText)
        {
            _levelAbilityText.text = _currentAbility.GetCurrentStatsAbility().ToString();
        }
    }


    /// <summary>
    /// закрывает панель выбора абилки и продолжает время.
    /// </summary>
    public void ClosePanel()
    {
        if (_panelChooseAbility)
        {
            _panelChooseAbility.SetActive(false);
            RemoveAbilityUIChooseUI();
            Time.timeScale = 1;
        }
   
    }


    /// <summary>
    /// проверка абилки на готовность если готова то можно испольозвать плюс блокирует кнопку на использование
    /// </summary>
    public void Update()
    {

        if (_currentAbility)
        {
            if (_currentAbility.state == Ability.AbilityState.Cooldown)
            {
                _buttonUseAbility.interactable = false;
                _timerCooldown.fillAmount = _currentAbility.cooldownTime / _currentAbility.Cooldown;
            }
            else if (_currentAbility.state == Ability.AbilityState.Ready)
            {
                _buttonUseAbility.interactable = true;
            }
        }


    }


}
