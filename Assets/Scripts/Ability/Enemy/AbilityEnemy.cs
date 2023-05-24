
using UnityEngine;

public abstract class AbilityEnemy : ScriptableObject
{
    public string AbilityName;
    public float ÑhargingTimer;
    public float DistanceAttack;
    public abstract void Activate(GameObject parentObject, float damage);
    public abstract void EnableAbility();

    public abstract void PreparationAbilityy(GameObject parent);

}
