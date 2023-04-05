using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<PlayerCharacteristics> _playerCharacteristicsList;
    [SerializeField] private PlayerDataManager _playerDataManager;

    private void Start()
    {


        if (_playerDataManager == null)
        {
            Debug.LogWarning("PlayerDataManager not found in the scene, creating a new one...");
            _playerDataManager = new GameObject("PlayerDataManager").AddComponent<PlayerDataManager>();
        }

        foreach (var playerCharacteristics in _playerCharacteristicsList)
        {
            var playerData = new PlayerData(playerCharacteristics.Speed, playerCharacteristics.BaseAttack, playerCharacteristics.MaxHealth, playerCharacteristics.Coins, playerCharacteristics.Name, false);

            if (!_playerDataManager.PlayerExists(playerCharacteristics.Name))
            {
                _playerDataManager.AddPlayer(playerCharacteristics.Name, playerData);
            }
        }

    }

    public void Upgrade(float healthIncrease, float damageIncrease, float speedIncrease, int coinsSpent, PlayerCharacteristics Character)
    {
        // Получаем данные текущего игрока
        PlayerData playerData = _playerDataManager.GetPlayerData(Character.Name);

        // Изменяем значения характеристик
        playerData.IncreaseMaxHealth(healthIncrease);
        playerData.IncreaseBaseAttack(damageIncrease);
        playerData.IncreaseSpeed(speedIncrease);
        playerData.DecreaseCoins(coinsSpent);
       
        // Обновляем данные на игровом объекте
        Character.SetPlayerData(playerData);

        // Сохраняем измененные данные
        playerData.Save();
        _playerDataManager.Save();
    }


    private void OnDisable()
    {
        _playerDataManager.Save();
    }

}
