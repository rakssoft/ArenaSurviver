
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Health", order = 51)]
public class HealthAbility : Ability
{
    private int _levelAbility;
    private int _multiplier;



    private void OnEnable()
    {
        EventManager.HealtAbilityLevelUp += LevelUp;
    }
    private void OnDisable()
    {
        EventManager.HealtAbilityLevelUp -= LevelUp;
    }

    public override void Activate(GameObject parent)
    {
        _levelAbility = LevelAbility;
        EventManager.TakeDamagePlayer(_levelAbility * _multiplier);
    }

    private void LevelUp(int level)
    {
        _multiplier = level;        
    }
}
