using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Enemy/ShockWave")]
public class ShockWaveAbilityEnemy : AbilityEnemy
{
    [SerializeField] private GameObject _prefabShockWave;
    [SerializeField] private float _damage;
    [SerializeField] private float _forceMove;
    [SerializeField] private float _timerForFalse;
    [SerializeField] private float _shockForce;
    [SerializeField] private GameObject _aoePrefab;

    public override void Activate(GameObject parent, float damage)
    {
        damage += _damage;
        GameObject prefabAttack = Instantiate(_prefabShockWave, parent.transform.position, Quaternion.identity);
        prefabAttack.AddComponent<MoveAbilityObject>();
        prefabAttack.AddComponent<TakeDamageEnemy>();
        prefabAttack.GetComponent<MoveAbilityObject>().StartObject(damage, parent.transform.position, _forceMove, _timerForFalse);
        prefabAttack.GetComponent<TakeDamageEnemy>().KnockbackForce(_shockForce);

    }

    public override void PreparationAbilityy(GameObject parent)
    {
        GameObject AoeArea = Instantiate(_aoePrefab, parent.transform.position, Quaternion.identity);
        parent.GetComponent<CharacteristicsEnemy>().IsAoeAttack();
    }

    public override void EnableAbility()
    {
        Debug.Log("enableFire");
    }
}