using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public static UnityAction<Vector3, string> TakeDamage;
    public static UnityAction<GameObject> TakeDamageIsOff;
    public static UnityAction<int> CurrentCountEnemy;
    public static UnityAction<bool> LevelUp;
    public static UnityAction<float> TakeDamagePlayer;
    public static UnityAction<int> HealtAbilityLevelUp;
    public static UnityAction<int> FireballAbilityLevelUp;
    public static UnityAction<Ability> AbilityAddUiFooterPanel;
    public static UnityAction<Ability> AbilityLevelUPUiFooterPanel;
                            
}
