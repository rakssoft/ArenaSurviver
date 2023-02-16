using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Image _valueExperience;
    public float SpeedMove;
    public float SpeedRotations;
    public float Health;
    public float Experience;
    [SerializeField] private int _level;
    public float FireRate;
    private float Armor;



    private void OnEnable()
    {
        EventManager.ExperienceDropEnemy += TakeExperience;
    }

    private void OnDisable()
    {
        EventManager.ExperienceDropEnemy -= TakeExperience;
    }
    private void Start()
    {
        _level = 1;
        Experience = 0;
        _valueExperience.fillAmount = 0;
    }


    public void TakeDamage(float damage)
    {

    }

    public void TakeExperience(float expa)
    {
        Experience += expa;
        _valueExperience.fillAmount = Experience / _level;
        if(Experience >= _level)
        {
            _level *= 2;
            Experience = 0;
            _valueExperience.fillAmount = 0;
        }
    }
}
