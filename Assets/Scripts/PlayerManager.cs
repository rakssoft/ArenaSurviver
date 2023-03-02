using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _skillPanel;
    [SerializeField] private SkillLevel _skillUiPrefab;
    [SerializeField] private Transform _parentFooter;
    [SerializeField] private List<SkillLevel> _listSkillLevels = new List<SkillLevel>();

    private void OnEnable()
    {
        EventManager.LevelUp += IsLevelUp;
        EventManager.AbilityAddUiFooterPanel += ShowAbilityFooterPanel;
    }

    private void OnDisable()
    {
        EventManager.LevelUp -= IsLevelUp;
        EventManager.AbilityAddUiFooterPanel -= ShowAbilityFooterPanel;
    }
    private void Start()
    {
        Time.timeScale = 1;
        _skillPanel.SetActive(false);
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
