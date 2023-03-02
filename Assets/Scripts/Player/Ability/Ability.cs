using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string AbilityName;
    public Sprite Icon;
    public AudioClip Sound;
    public float Cooldown;
    public float Duration;
    public int Level;
    public AbilityState state = AbilityState.Ready;
    public float activeTime;
    public float cooldownTime;

    public abstract void Activate(GameObject parentObject);
    public abstract void LevelUp();

    public enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
}
