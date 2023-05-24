using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowCharacterMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Image _experienceBar;
    [SerializeField] private CharacterManager _characterManager;    
    [SerializeField] private  List<CharacterCharacteristics>  _characterCharacteristics = new List<CharacterCharacteristics>();
    [SerializeField] private GameObject _spawnPositionCharacter;
    [SerializeField] private CharacterDataManager _characterDataManager;
    [SerializeField] private float _upgradeCharacterPrice;
    private CharacterCharacteristics _character;
    private GameObject characterObject;
    private int _characterChooseActive;
    private CharacterData _characterData;

    private void OnEnable()
    {
        EventManager.AddCharacterCharacteristics += AddCharactersInChoose;
    }
    private void OnDisable()
    {
        EventManager.AddCharacterCharacteristics -= AddCharactersInChoose;
    }

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
        if (!PlayerPrefs.HasKey("activeCharacterName"))
        {         
            PlayerPrefs.SetString("activeCharacterName", GetPlayerData().playerName);
        }
       
    }
    public void ShowCharackers()
    {
        ShowUI(GetActiveCharacter());
        GetPlayerData();
    }
    public void CloseShowUI()
    {
        if (characterObject)
        {
            Destroy(characterObject);
        }
    }

    public void ChooseCharacter(int index)
    {
        if (_characterCharacteristics.Count == 1)
        {
            _characterChooseActive = 0;
            return;
        }

        _characterChooseActive += index;

        if (_characterChooseActive < 0)
        {
            _characterChooseActive = _characterCharacteristics.Count - 1;
            PlayerPrefs.SetInt("activeCharacter", _characterChooseActive);
        }

        if (_characterChooseActive > _characterCharacteristics.Count - 1)
        {
            _characterChooseActive = 0;
            PlayerPrefs.SetInt("activeCharacter", _characterChooseActive);
        }

        PlayerPrefs.SetInt("activeCharacter", _characterChooseActive);
        PlayerPrefs.SetString("activeCharacterName", GetPlayerData().playerName);
        GetPlayerData();
        ShowUI(GetActiveCharacter());
    }


    public void AddCharactersInChoose(CharacterCharacteristics characterCharacteristics)
    {
        _characterCharacteristics.Add(characterCharacteristics);
    }
    private int GetActiveCharacter()
    {
        return PlayerPrefs.GetInt("activeCharacter");
    }
    public CharacterData GetPlayerData()
    {
        _characterData = _characterDataManager.GetPlayerData(_characterCharacteristics[GetActiveCharacter()].Name);
        return _characterData;
    }
    public CharacterCharacteristics GetPlayerCharacteristcs()
    {
        _character = _characterCharacteristics[GetActiveCharacter()];
        return _character;
    }

    /// <summary>
    /// Отображает персонажа по индексу в списке.
    /// </summary>
    /// <param name="indexPlayers"></param>
    private void ShowUI(int indexPlayers)
    {
        CloseShowUI();
        _name.text = _characterCharacteristics[indexPlayers].Name.ToString();
        _health.text = _characterCharacteristics[indexPlayers].MaxHealth.ToString("F0");
        _damage.text = _characterCharacteristics[indexPlayers].BaseAttack.ToString("F0");
        _speed.text = _characterCharacteristics[indexPlayers].Speed.ToString("F0");
        _level.text = _characterCharacteristics[indexPlayers].Level.ToString();

        float currentExperience = _characterCharacteristics[indexPlayers].Experience;
        float experienceNeeded = _characterCharacteristics[indexPlayers].ExperienceNeededForLevel;

        float fillAmount = currentExperience / experienceNeeded;
        if (fillAmount >= 1f) // Если fillAmount превышает 1, установим его равным 1
        {
            fillAmount = 0f;
        }

        _experienceBar.fillAmount = fillAmount;

        _character = _characterCharacteristics[indexPlayers]; 
        Vector3 direction = Camera.main.transform.position - _spawnPositionCharacter.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        characterObject = Instantiate(_character.PrefabCharacter, _spawnPositionCharacter.transform.position, rotation);
    }

    /// <summary>
    /// Тестовая функци для пробы апгрейда персонажа. На нее можно будет что то сделать
    /// </summary>
    public void Upgrade()
    {
        float coinCount = Wallet.Instance.coins;

        if (coinCount >= _upgradeCharacterPrice)
        {
            Wallet.Instance.RemoveCoins(_upgradeCharacterPrice);
            CloseShowUI();
            CharacterCharacteristics character = GetPlayerCharacteristcs();
            _characterManager.Upgrade(character, 5);
            ShowUI(GetActiveCharacter());
            EventManager.PurchaseIsCompleted?.Invoke();
        }
        else
        {
            Debug.Log("no money upgrade character");
        }
    }




}
