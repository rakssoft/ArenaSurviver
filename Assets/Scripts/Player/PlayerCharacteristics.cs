using UnityEngine.UI;
using UnityEngine;

public class PlayerCharacteristics: MonoBehaviour
{
    [SerializeField] private Image _valueExperience;
    [SerializeField] private Image _valueHealth;
    [SerializeField] private Text _levelText;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;



    private int _level;
    private float _currentHealt;
    private float _experience;   
    private float _needExperienceForLevel;


   
    public float SpeedRotations;
    public float FireRate;
    public float Speed
    {
        get { return _speed; }
        private set { _speed = value; }
    }



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
        _currentHealt = _maxHealth;
        _level = 1;
        _needExperienceForLevel = _level;
        _experience = 0;
        _valueExperience.fillAmount = 0;
        _valueHealth.fillAmount = 1;
        TakeDamage(0);
    }

    private void ShowInfo()
    {
        _valueHealth.fillAmount = _currentHealt / _maxHealth;
        _valueExperience.fillAmount = _experience / _needExperienceForLevel;
        _levelText.text = _level.ToString();
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
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

    public void IncreaseMaxHealth(int health)
    {
        _maxHealth += health;
    }




}
