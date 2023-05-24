
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Enemy/Melee")]
public class MeleeAbilityEnemySO : AbilityEnemy
{
    [SerializeField] private GameObject _prefabMeleeAttack;
    [SerializeField] private float _damage;
    [SerializeField] private float _forceMove;
    [SerializeField] private float _timerForFalse;


    public override void Activate(GameObject parent, float damage)
    {
        damage += _damage;
        GameObject prefabAttack = Instantiate(_prefabMeleeAttack, parent.transform.position, Quaternion.identity);
        prefabAttack.AddComponent<MoveAbilityObject>();
        prefabAttack.AddComponent<TakeDamageEnemy>();
        prefabAttack.GetComponent<MoveAbilityObject>().StartObject(damage, parent.transform.position, _forceMove, _timerForFalse);

    }

    public override void PreparationAbilityy(GameObject parent)
    {
        
    }

    public override void EnableAbility()
    {
        Debug.Log("enableFire");
    }
}