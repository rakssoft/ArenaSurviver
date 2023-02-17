using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _skillPanel;
    private int _healthLevel;

    private void OnEnable()
    {
        EventManager.LevelUp += IsLevelUp;
    }

    private void OnDisable()
    {
        EventManager.LevelUp -= IsLevelUp;
    }
    private void Start()
    {
        _healthLevel = 0;
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

    public void HealthLevelUp()
    {
        _healthLevel++;
        EventManager.HealtAbilityLevelUp?.Invoke(_healthLevel);
        SkillPanelOff();
    }

}
