using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
     [SerializeField] private GameObject[]  _listPanels;
    [SerializeField] private int _indexStartPanel;

    private void Start()
    {
        for (int i = 0; i < _listPanels.Length; i++)
        {
            _listPanels[i].SetActive(false);
        }
        _listPanels[_indexStartPanel].SetActive(true);
    }

    public void OnPanel(int indexPanel)
    {
        for (int i = 0; i < _listPanels.Length; i++)
        {
            _listPanels[i].SetActive(false);
        }
        _listPanels[indexPanel].SetActive(true);
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
