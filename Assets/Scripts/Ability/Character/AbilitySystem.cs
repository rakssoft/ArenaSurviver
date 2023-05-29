using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AbilitySystem : MonoBehaviour
{
   
    [SerializeField] private CharacterCharacteristics _characterCharacteristics;
    [SerializeField] private TextMeshProUGUI _cooldownText;

    public List<Ability> AbilitiesList;

    private void Start()
    {
        foreach (var ability in AbilitiesList)
        {
            ability.EnableAbility(_characterCharacteristics.GetBaseDamage());
         //   EventManager.AddAbilityInUiFooterPanel?.Invoke(ability);
        }
    }

    private void OnEnable()
    {
        EventManager.AddAbility += AddAbility;
        EventManager.UseAbality += ActivateAbilityButton;
    }

    private void OnDisable()
    {
        EventManager.AddAbility -= AddAbility;
        EventManager.UseAbality -= ActivateAbilityButton;
    }

    public void ActivateAbilityButton(Ability ability)
    {

        if (ability.state == Ability.AbilityState.Ready)
        {
            ability.state = Ability.AbilityState.Active;
            ability.Activate(gameObject);
            ability.activeTime = ability.Duration;
        }

    }

    private void Update()
    {
        foreach (var ability in AbilitiesList)
        {
            switch (ability.state)
            {
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
        if (!AbilitiesList.Contains(ability))
        {
            AbilitiesList.Add(ability);
            ability.EnableAbility( _characterCharacteristics.GetBaseDamage());
        }
    }

    public CharacterCharacteristics GetCharacterCharacteristics()
    {
        return _characterCharacteristics;
    }



}
