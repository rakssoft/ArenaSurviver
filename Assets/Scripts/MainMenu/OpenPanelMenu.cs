using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanelMenu : MonoBehaviour
{
    [SerializeField] private Animator _chooseCharacter;
    [SerializeField] private Animator _footer;
    [SerializeField] private Animator _stats;
    [SerializeField] private Animator _upgradeCharacter;
    [SerializeField] private Animator _settingsPanel;
    [SerializeField] private Animator _battlePanel;

    [SerializeField] private GameObject[] _modBattleObjects;

    private string _modBattle = "modBattle";
    private void Start()
    {
      if(!PlayerPrefs.HasKey(_modBattle))
        {
            PlayerPrefs.SetInt(_modBattle, 0);
        }

        EnableModBattle(PlayerPrefs.GetInt(_modBattle));
    }

    public void EnableModBattle(int index)
    {
        for (int i = 0; i < _modBattleObjects.Length; i++)
        {
            _modBattleObjects[i].SetActive(false);
        }
        _modBattleObjects[index].SetActive(true);
        PlayerPrefs.SetInt(_modBattle, index);

    }





    public void PanelChooseCharacter()
    {
        _footer.SetBool("Close", true);
        _chooseCharacter.SetBool("Close", false);
        _stats.SetBool("Close", true);
        _upgradeCharacter.SetBool("Close", true);
        _chooseCharacter.SetBool("Upgrade", false);
    }
    public void PanelUpgradeCharacter()
    {
        _chooseCharacter.SetBool("Close", false);
        _upgradeCharacter.SetBool("Close", false);
        _stats.SetBool("Close", false);
        _footer.SetBool("Close", true);
        _chooseCharacter.SetBool("Upgrade", true);

    }
    public void PanelMain()
    {
        _chooseCharacter.SetBool("Upgrade", false);
        _upgradeCharacter.SetBool("Close", true);
        _stats.SetBool("Close", true);
        _footer.SetBool("Close", false);
        _chooseCharacter.SetBool("Close", true);
    }
    public void SettingsPanelClose()
    {
        _settingsPanel.SetBool("Close", true);
    } 
    public void SettingsPanelOpen()
    {
        _settingsPanel.SetBool("Close", false);
    }  
    public void BattlePanelClose()
    {
        _battlePanel.SetBool("Close", true);
    } 
    public void BattlePanelOpen()
    {
        _battlePanel.SetBool("Close", false);
    }
}
