using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/IncreaseMaxiHealth")]
public class IncreaseMaxiHealthAbility : Ability
{
    [SerializeField] private int _baseIncreaseHealth;
    [SerializeField] private int _currentIncreaseHealth;

    private void OnEnable()
    {
        _currentIncreaseHealth = _baseIncreaseHealth;
    }
    public override void Activate(GameObject parent)
    {
        if (parent.TryGetComponent(out PlayerCharacteristics player))
        {
            player.IncreaseMaxHealth(_currentIncreaseHealth);
        }
    }


    public override void LevelUp()
    {
        int NumberToIncrease = 10;
        _currentIncreaseHealth += NumberToIncrease ;
    }
}
