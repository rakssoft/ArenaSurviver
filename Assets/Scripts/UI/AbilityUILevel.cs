using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUILevel : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private Image _abilityImage;



    public void ShowAbility(Ability ability)
    {
        _lock.SetActive(false);
        _abilityImage.sprite = ability.Icon;
    }

    public void HideAbility()
    {
        _lock.SetActive(true);
    }
}
