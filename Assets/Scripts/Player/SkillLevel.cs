using UnityEngine.UI;
using UnityEngine;

public class SkillLevel : MonoBehaviour
{
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _imageSkill;
    private int _level;


    private void Start()
    {
        _level = 0;
        ShowSkill();
    }
    private void ShowSkill()
    {
        if(_level == 0)
        {
            _levelText.text = "";
            Color newColor = _imageSkill.color;
            newColor.a = 0.5f;
            _imageSkill.color = newColor;

        }
        else if(_level > 0)
        {
            _levelText.text = _level.ToString();
            Color newColor = _imageSkill.color;
            newColor.a = 1f;
            _imageSkill.color = newColor;
        }
    }

    public void LevelUP(int level)
    {
        _level += level;
        ShowSkill();
    }




}
