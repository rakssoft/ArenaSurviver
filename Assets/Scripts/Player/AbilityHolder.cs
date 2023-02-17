using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public List<Ability> abilitys;
    private float _cooldownTime;
    private float _activeTime;


    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;



    private void Start()
    {
        foreach (var item in abilitys)
        {
          if(  item.LevelAbility > 0)
            {

            }
        }
    }
    private void Update()
    {
        for (int i = 0; i < abilitys.Count; i++)
        {
            switch (state)
            {
                case AbilityState.ready:
                    abilitys[i].Activate(gameObject);
                    state = AbilityState.active;
                    _activeTime = abilitys[i].ActiveTime;
                    break;
                case AbilityState.active:
                    if (_activeTime > 0)
                    {
                        _activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.cooldown;
                        _cooldownTime = abilitys[i].CooldownTime;
                    }
                    break;
                case AbilityState.cooldown:
                    if (_cooldownTime > 0)
                    {
                        _cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.ready;
                    }
                    break;
            }
        }  
    }
    public void AddAbility(Ability ability)
    {
        int coutAbility = 0;
        foreach (var abil in abilitys)
        {
            if (abil == ability)
            {
                coutAbility++;
            }
        }
        if(coutAbility == 0)
        {
            abilitys.Add(ability);
        }
    }

}
