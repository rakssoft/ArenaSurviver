
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string Name;
    public float CooldownTime;
    public float ActiveTime;
    public int LevelAbility;


    public virtual void Activate(GameObject parent) { }
}
