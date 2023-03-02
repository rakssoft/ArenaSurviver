using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public List<Ability> abilities;

    private void Start()
    {
        foreach (var ability in abilities)
        {
            EventManager.AbilityAddUiFooterPanel?.Invoke(ability);
        }
    }

    private void Update()
    {
        foreach (var ability in abilities)
        {
            switch (ability.state)
            {
                case Ability.AbilityState.Ready:

                        ability.state = Ability.AbilityState.Active;
                        ability.Activate(gameObject);
                        ability.activeTime = ability.Duration;
                    
                    break;
                case Ability.AbilityState.Active:
                    if (ability.activeTime > 0)
                    {
                        ability.activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        ability.state = Ability.AbilityState.Cooldown;
                        ability.cooldownTime = ability.Cooldown;
                    }
                    break;
                case Ability.AbilityState.Cooldown:
                    if (ability.cooldownTime > 0)
                    {
                        ability.cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        ability.state = Ability.AbilityState.Ready;
                    }
                    break;
            }
        }
    }

    public void AddAbility(Ability ability)
    {
        if (!abilities.Contains(ability))
        {
            abilities.Add(ability);
            EventManager.AbilityAddUiFooterPanel?.Invoke(ability);
        }
    }

    public void LevelUpAbility(Ability ability)
    {
        ability.LevelUp(); 
        EventManager.AbilityLevelUPUiFooterPanel?.Invoke(ability);
    }
}
