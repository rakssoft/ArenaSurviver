using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/RestorinHealth")]
public class RestorinHealthAbility : Ability
{
    [SerializeField] private int _baseRestoringHealth;
    [SerializeField] private int _currentRestoringHealth;

    public override void Activate(GameObject parent)
    {
         if(_currentRestoringHealth < _baseRestoringHealth)
        {
            _currentRestoringHealth = _baseRestoringHealth;
        }

        Debug.Log(_currentRestoringHealth);
    }

    public override void LevelUp()
    {      
        _currentRestoringHealth ++;
  //      EventManager.AbilityLevelUPUiFooterPanel?.Invoke(this);
    }

    private void OnEnable()
    {
        _currentRestoringHealth = Level;
    }

}
