using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private PlayerCharacteristics _playerCharacteristics;
    [SerializeField] private Image _valueHealth;
    private float _maxHealth;
    private float _currentHealt;

              

    private void OnEnable()
    {
        EventManager.TakeDamagePlayer += TakeDamage;
    }

    private void OnDisable()
    {
        EventManager.TakeDamagePlayer += TakeDamage;
    }

    private void Start()
   {
     _maxHealth = _playerCharacteristics.MaxHealth;
     _currentHealt = _maxHealth;
    _valueHealth.fillAmount = 1;
    TakeDamage(0);
   }

    public void TakeDamage(float damage)
    {    
        _currentHealt += damage;
        ShowInfo();
        if (_currentHealt <= 0)
        {
            EventManager.BatttleIsWon(false);
        }
        else if(_currentHealt >= _maxHealth)
        {
            _currentHealt = _maxHealth;
        }
    }

      private void ShowInfo()
    {
        _valueHealth.fillAmount = _currentHealt / _maxHealth;
    }

    public void IncreaseMaxHealth(float IncreaseHealth)
    {
        _currentHealt += IncreaseHealth;
      if(  _currentHealt > _maxHealth)
        {
            _currentHealt = _maxHealth;
        }
    }
}
