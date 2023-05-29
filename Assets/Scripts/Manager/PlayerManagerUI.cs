using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class PlayerManagerUI : MonoBehaviour
{
    [SerializeField] private PlayerController[] _characters;
    [SerializeField] private GameObject _spawnPlayer;
    [SerializeField] private Transform _parentPlayer;
    [SerializeField] private SkillLevel __baseAbility;  
    [SerializeField] private Transform _parentFooter; 
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private AbilityCreateBattle _abilityCreateBattle;

    private CharacterCharacteristics characterCharacteristics;
    private AbilitySystem abilitySystem;

    private void Start()
    {
        Time.timeScale = 1;        
        PlayerController player = Instantiate(_characters[ChooseCharacters()], _spawnPlayer.transform.position, Quaternion.identity, _parentPlayer);
        virtualCamera.Follow = player.transform;
        characterCharacteristics = player.gameObject.GetComponent<AbilitySystem>().GetCharacterCharacteristics();
        abilitySystem = player.gameObject.GetComponent<AbilitySystem>();
        _abilityCreateBattle.ShowUIButtonsAbility(abilitySystem, characterCharacteristics);
    }

    private int ChooseCharacters()
    {
        string name = PlayerPrefs.GetString("activeCharacterName");
        for (int i = 0; i < _characters.Length; i++)
        {
            if (name == _characters[i].name)
            {
                return i;
            }
        }
        return 0;
    }






}
