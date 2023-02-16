using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private GameObject[] _pointsSpawn;
    [SerializeField] private Transform _parentEnemy;
    [SerializeField] private float _timerSpawn;
    [SerializeField] private int _waveEnemy;
    private float _timer;

    private void Start()
    {
        _waveEnemy = 1;
        _timer = _timerSpawn;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            _timer = _timerSpawn;
            _waveEnemy++;
            SpawnEnemy(_waveEnemy);
        }
    }


    private void SpawnEnemy(int wave)
    {
        for (int i = 0; i < _waveEnemy * 3; i++)
        {
            Instantiate(_enemyPrefabs[RandomEnemy()], _pointsSpawn[RandomPoint()].transform.position, Quaternion.identity, _parentEnemy);
            EventManager.CurrentCountEnemy?.Invoke(1);
        }
    }

    private int RandomPoint()
    {
        return Random.Range(0, _pointsSpawn.Length);
    }
    private int RandomEnemy()
    {
        return Random.Range(0, _enemyPrefabs.Length);
    }
}


