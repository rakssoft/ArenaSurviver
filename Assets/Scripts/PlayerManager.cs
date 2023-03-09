using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _spawnPlayer;
    [SerializeField] private Transform _parentPlayer;
    [SerializeField] private GameObject _skillPanel;
    [SerializeField] private SkillLevel _skillUiPrefab;
    [SerializeField] private Transform _parentFooter;
    [SerializeField] private List<SkillLevel> _listSkillLevels = new List<SkillLevel>();
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void OnEnable()
    {
        EventManager.LevelUp += IsLevelUp;
        EventManager.AddAbilityInUiFooterPanel += ShowAbilityFooterPanel;
    }

    private void OnDisable()
    {
        EventManager.LevelUp -= IsLevelUp;
        EventManager.AddAbilityInUiFooterPanel -= ShowAbilityFooterPanel;
    }
    private void Start()
    {
        Time.timeScale = 1;
        _skillPanel.SetActive(false);
        PlayerController player = Instantiate(_player, _spawnPlayer.transform.position, Quaternion.identity, _parentPlayer);
        virtualCamera.Follow = player.transform;
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
        EventManager.AddAbility?.Invoke(ability);
        ability.LevelUp();
        EventManager.AbilityLevelUPUiFooterPanel?.Invoke(ability);
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
