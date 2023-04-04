using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Gear gear;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerCharacteristics playerCharacteristics;
    [SerializeField] private PlayerDataManager dataManager;





    public void EquipGear()
    {
        playerData = dataManager.GetPlayerData(playerData.playerName);
        gear.Equip(playerData, playerCharacteristics);
    }
}
