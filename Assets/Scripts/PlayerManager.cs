using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _skillPanel;



    private void Start()
    {
        Time.timeScale = 1;
        _skillPanel.SetActive(false);
    }


    private void IsLevelUp()
    {
        Time.timeScale = 0;
        _skillPanel.SetActive(true);
    }


    public void SkillPanelOff()
    {
        Time.timeScale = 1;
        _skillPanel.SetActive(false);
    }

}
