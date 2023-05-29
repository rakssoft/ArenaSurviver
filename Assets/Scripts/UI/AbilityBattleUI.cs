using UnityEngine;
using UnityEngine.UI;

public class AbilityBattleUI : MonoBehaviour
{
    [SerializeField] private string _abilityName;
    [SerializeField] private Image _imageAbility;
    [SerializeField] private Ability _ability;

    public void ShowAbilityUI(Sprite icon, Ability ability)
    {
        _imageAbility.sprite = icon;
        _ability = ability;
    }

    public void OnButtonClick()
    {
        EventManager.AddAbility?.Invoke(_ability);
    }
}
