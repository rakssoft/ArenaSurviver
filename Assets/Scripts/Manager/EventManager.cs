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
    public static UnityAction<Ability> AddAbilityInUiFooterPanel;
    public static UnityAction<Ability> AbilityLevelUPUiFooterPanel;
    public static UnityAction<Ability> AddAbility;
    public static UnityAction<int> IndexAudioClipPlay;
    public static UnityAction<bool> BatttleIsWon;
    public static UnityAction SaveDataBase;
    public static UnityAction<Gear> EquipGearEvent;
    public static UnityAction<Gear> UnEquipGearEvent;
    public static UnityAction<Gear> PurchaseGearCompleted;
    public static UnityAction<CharacterCharacteristics> AddCharacterCharacteristics;
    public static UnityAction PurchaseIsCompleted;
    public static UnityAction<float> TakeDamageBoss;
    public static UnityAction<Ability> UseAbality;
    public static UnityAction CharacterAbilityLevelUp;


}
