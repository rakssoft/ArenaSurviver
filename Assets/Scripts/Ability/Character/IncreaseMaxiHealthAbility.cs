using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/IncreaseMaxiHealth")]
public class IncreaseMaxiHealthAbility : Ability
{
    [SerializeField] private int _currentLevel;
    [SerializeField] private int _baseIncreaseHealth;
    private int _currentIncreaseHealth;

    public override void Activate(GameObject parent)
    {
        if (parent.TryGetComponent(out Health player))
        {
            player.IncreaseMaxHealth(_currentIncreaseHealth);
        }
    }

    public override void LevelUp()
    {
        int NumberToIncrease = 20;
        _currentIncreaseHealth += NumberToIncrease ;
    }

    public override void EnableAbility(float Damage)
    {
        _currentIncreaseHealth = _baseIncreaseHealth;
    }
    public override int GetCurrentStatsAbility()
    {
        return _currentLevel;
    }
}
