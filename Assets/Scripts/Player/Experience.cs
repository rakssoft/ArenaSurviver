using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
     [SerializeField] private Text _levelText;
    [SerializeField] private Image _valueExperience;
    [SerializeField] private PlayerCharacteristics _playerCharacteristics;
    private float _experience;   
    private float _needExperienceForLevel;  
    private int _level;


    private void Start(){
          _level = 1;
        _needExperienceForLevel = _level;
        _experience = 0;
        _valueExperience.fillAmount = 0;
    }

    private void ShowInfo()
    {
        _valueExperience.fillAmount = _experience / _needExperienceForLevel;
        _levelText.text = _level.ToString();
    }

       public void TakeExperience(float expa)
    {
        _experience += expa;        
        if(_experience >= _needExperienceForLevel)
        {
            _level ++;
            _needExperienceForLevel *= 2;
            _experience = 0;
            _valueExperience.fillAmount = 0;
            EventManager.LevelUp?.Invoke(true);
            
        }
        ShowInfo();
    }


}
