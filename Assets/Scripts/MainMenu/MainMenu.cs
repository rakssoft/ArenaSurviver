using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject[]  _listPanels;
    [SerializeField] private GameObject[] _activeButtonsPanel;
    [SerializeField] private int _indexStartPanel;

    private void Start()
    {
        OffPanel();
        _listPanels[_indexStartPanel].SetActive(true);
        _activeButtonsPanel[_indexStartPanel].SetActive(true);
    }

    public void OnPanel(int indexPanel)
    {
        OffPanel();
        _listPanels[indexPanel].SetActive(true);
        if (indexPanel < _activeButtonsPanel.Length)
        {
            _activeButtonsPanel[indexPanel].SetActive(true);
        }

    }

    private void OffPanel()
    {
        for (int i = 0; i < _activeButtonsPanel.Length; i++)
        {
            _activeButtonsPanel[i].SetActive(false);
        }
        for (int i = 0; i < _listPanels.Length; i++)
        {
            _listPanels[i].SetActive(false);
        }

    }


    public void SwitchCharacterOff()
    {
        _characterPrefab.SetActive(false);
    }

    public void SwitchCharacterOn()
    {
        _characterPrefab.SetActive(true);
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
