using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacteristics", menuName = "Player/Characteristics")]
public class PlayerCharacteristics : ScriptableObject
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private bool _unlocked;
    public float Speed => _playerData.speed;
    public float BaseAttack => _playerData.baseAttack;
    public float MaxHealth => _playerData.health;
    public int Coins => _playerData.coins;
    public string Name;
    public GameObject PrefabCharacter;
    public bool Unlocked => _unlocked;

    private void OnEnable()
    {
        string playerName = Name; // имя игрока можно получить из настроек
        _playerData = new PlayerData(10.0f, 5, 100, 0, playerName, false);
        _playerData.Load();
    }

    private void OnDisable()
    {
        if (_playerData != null)
        {
            // Сохранение данных в файл при выгрузке скриптабельного объекта
            _playerData.Save();
        }
    }
    public void SetPlayerData(PlayerData playerData)
    {
        _playerData.speed = playerData.speed;
        _playerData.baseAttack = playerData.baseAttack;
        _playerData.health = playerData.health;
        _playerData.coins = playerData.coins;
        _playerData.playerName = playerData.playerName;
        _playerData.gearList = playerData.gearList;
        Name = playerData.playerName;
    }
    public void Unlock()
    {
        _unlocked = true;
    }

  



}
