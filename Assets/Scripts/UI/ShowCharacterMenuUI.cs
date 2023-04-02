
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowCharacterMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerCharacteristics[] _playersCharacteristics;
    [SerializeField] private GameObject _spawnPositionCharacter;
    private PlayerCharacteristics _character;
    private GameObject characterObject;

    
    private int _characterChooseActive;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("activeCharacter"))
        {
            PlayerPrefs.SetInt("activeCharacter", 0);
        }
        else
        {
            _characterChooseActive = PlayerPrefs.GetInt("activeCharacter");
        }
    }

    public void ShowCharackers()
    {
        ShowUI(GetActiveCharacter());
    }

    public void ChooseCharacter(int index)
    {
        _characterChooseActive += index;
        if(_characterChooseActive <= 0)
        {
            _characterChooseActive = 0;
            PlayerPrefs.SetInt("activeCharacter", _characterChooseActive);

        }
        if(_characterChooseActive >= _playersCharacteristics.Length)
        {
            _characterChooseActive = _playersCharacteristics.Length -1;
            PlayerPrefs.SetInt("activeCharacter", _characterChooseActive);
        }
        PlayerPrefs.SetInt("activeCharacter", _characterChooseActive);

        ShowUI(GetActiveCharacter());
    }

    public void CloseShowUI()
    {
        if (characterObject)
        {
            Destroy(characterObject);
        }       
    }

    public void Upgrade()
    {
        CloseShowUI();
        PlayerCharacteristics player = _playersCharacteristics[GetActiveCharacter()];
        _playerManager.Upgrade(5,5,5,5, player);
        ShowUI(GetActiveCharacter());

    }

    private int GetActiveCharacter()
    {
        return PlayerPrefs.GetInt("activeCharacter");
    }

    private void ShowUI(int indexPlayers)
    {

        _name.text = _playersCharacteristics[indexPlayers].Name.ToString();
        _health.text = _playersCharacteristics[indexPlayers].MaxHealth.ToString("F0");
        _damage.text = _playersCharacteristics[indexPlayers].BaseAttack.ToString("F0");
        _speed.text = _playersCharacteristics[indexPlayers].Speed.ToString("F0");
        _character = _playersCharacteristics[indexPlayers];
        Vector3 direction = Camera.main.transform.position - _spawnPositionCharacter.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        characterObject = Instantiate(_character.PrefabCharacter, _spawnPositionCharacter.transform.position, rotation);
    }


}
