using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class PlayerManagerUI : MonoBehaviour
{
    [SerializeField] private PlayerController[] _characters;
    [SerializeField] private GameObject _spawnPlayer;
    [SerializeField] private Transform _parentPlayer;
    [SerializeField] private GameObject _skillPanel;
    [SerializeField] private SkillLevel _skillUiPrefab;
   
    [SerializeField] private Transform _parentFooter;
    [SerializeField] private List<SkillLevel> _listSkillLevels = new List<SkillLevel>();
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private Transform _parentSkill;
    [SerializeField] private AbilityBattleUI _abilityBattleUIPrefab;

    private CharacterCharacteristics characterCharacteristics;
    private void OnEnable()
    {
        EventManager.LevelUp += IsLevelUp;
        EventManager.AddAbilityInUiFooterPanel += ShowAbilityFooterPanel;
        EventManager.AddAbility += ChooseAbility;
    }

    private void OnDisable()
    {
        EventManager.LevelUp -= IsLevelUp;
        EventManager.AddAbilityInUiFooterPanel -= ShowAbilityFooterPanel;
        EventManager.AddAbility -= ChooseAbility;
    }
    private void Start()
    {
        Time.timeScale = 1;
        _skillPanel.SetActive(false);
        PlayerController player = Instantiate(_characters[ChooseCharacters()], _spawnPlayer.transform.position, Quaternion.identity, _parentPlayer);
        virtualCamera.Follow = player.transform;
        characterCharacteristics = player.gameObject.GetComponent<AbilitySystem>().GetCharacterCharacteristics();
        AddAbilityUIChooseUI(characterCharacteristics.CharacterAbilities);
    }

    private void AddAbilityUIChooseUI(List<Ability> characterAbility)
    {
        foreach (Ability ability in characterAbility)
        {
            AbilityBattleUI abilityUI =  Instantiate(_abilityBattleUIPrefab, _parentSkill);
            abilityUI.ShowAbilityUI(ability.Icon, ability);
        }
    }

    private int ChooseCharacters()
    {
        string name = PlayerPrefs.GetString("activeCharacterName");
        for (int i = 0; i < _characters.Length; i++)
        {
            if (name == _characters[i].name)
            {
                return i;
            }
        }
        return 0;
    }

    private void IsLevelUp(bool isLevelUp)
    {
        if(isLevelUp == true)
        {
            Time.timeScale = 0;
            _skillPanel.SetActive(true);
        }    
    }

    public void SkillPanelOff()
    {
        Time.timeScale = 1;
        _skillPanel.SetActive(false);
    }
 
    public void ChooseAbility(Ability ability)
    {        
        ability.LevelUp();
        EventManager.AbilityLevelUPUiFooterPanel?.Invoke(ability);
        SkillPanelOff();
    }

    public void ShowAbilityFooterPanel(Ability ability)
    {
        SkillLevel isActiveUIAbility = Instantiate(_skillUiPrefab, _parentFooter);
        isActiveUIAbility.ShowSkill(ability);
        AddListSkill(isActiveUIAbility);
    }

    private void AddListSkill(SkillLevel skillLevel)
    {
       
        _listSkillLevels.Add(skillLevel);
    }


}
