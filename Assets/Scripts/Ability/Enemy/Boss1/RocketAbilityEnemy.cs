using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Enemy/Rocket")]
public class RocketAbilityEnemy : AbilityEnemy
{
    [SerializeField] private MoveAbilityObject _prefabRocket;
    [SerializeField] private float _damage;
    [SerializeField] private float _forceMove;
    [SerializeField] private float _timerForFalse;

    public override void Activate(GameObject parent, float damage)
    {
        damage += _damage;
        Quaternion parentRotation = parent.transform.rotation;
        MoveAbilityObject bull = Instantiate(_prefabRocket, parent.transform.position, Quaternion.identity);
        bull.transform.rotation = parent.transform.rotation;        
        parent.transform.rotation = parentRotation;
        bull.StartObject(damage, parent.transform.position, _forceMove, _timerForFalse);
    }

    public override void EnableAbility()
    {
        Debug.Log("enableFire");
    }

    public override void PreparationAbilityy(GameObject parent)
    {
        // подготовка для выстрела анимация или чтото
    }
}
