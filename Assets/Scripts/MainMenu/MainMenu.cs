using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;


    private void Start()
    {
        _characterPrefab.SetActive(true);
        
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
