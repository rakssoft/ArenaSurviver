using UnityEngine.UI;
using UnityEngine;

public class SkillLevel : MonoBehaviour
{
    [SerializeField]  private Text _levelText;
    [SerializeField] private Image _imageSkill;
    [SerializeField] Ability _ability;
    [SerializeField] private int _level;

    private void OnEnable()
    {
        EventManager.AbilityLevelUPUiFooterPanel += LevelUpShow;
    }

    private void OnDisable()
    {

        EventManager.AbilityLevelUPUiFooterPanel -= LevelUpShow;
    }
    public void ShowSkill(Ability ability)
    {
        _ability = ability;
        _level = _ability.Level;
        _imageSkill.sprite = _ability.Icon;
        _levelText.text = _level.ToString();

    }

    public void LevelUpShow(Ability ability)
    {
        if(_ability == ability)
        {
            _level++;
            _levelText.text = _level.ToString();
        }
        
    }

}
