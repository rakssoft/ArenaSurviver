using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] private CharacterCharacteristics _characterCharacteristics;
    [SerializeField] private Image _valueHealth;
    private float _maxHealth;
    private float _currentHealt;

              

    private void OnEnable()
    {
        EventManager.TakeDamagePlayer += TakeDamage;
    }

    private void OnDisable()
    {
        EventManager.TakeDamagePlayer -= TakeDamage;
    }

    private void Start()
   {
     _maxHealth = _characterCharacteristics.MaxHealth;
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
        if (_valueHealth == null)
        {
            Debug.Log("NOT IMAGE HEALTH");
            return;
        }
        float fillAmount = _currentHealt / _maxHealth;
        _valueHealth.fillAmount = fillAmount;
    }

    public float GetCurrentHealth()
    {
        return _currentHealt;
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
