using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawnEnemy : MonoBehaviour
{
    [SerializeField] private CharacteristicsEnemy [] _enemyPrefabs;
    [SerializeField] private GameObject[] _pointsSpawn;
    [SerializeField] private Transform _parentEnemy;
    [SerializeField] private float _timerSpawn;
    [SerializeField] private int _waveEnemy;
    [SerializeField] private bool _isBossScene;
    private float _timer;

    private void Start()
    {
        _timer = _timerSpawn;
        if (_isBossScene)
        {
            Instantiate(_enemyPrefabs[0], _pointsSpawn[RandomPoint()].transform.position, Quaternion.identity, _parentEnemy);
        }
        else
        {
            SpawnEnemy(_waveEnemy);
        }
    }

    private void Update()
    {
        if (!_isBossScene)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = _timerSpawn;
                _waveEnemy++;
                SpawnEnemy(_waveEnemy);
            }
        }
    }



    private void SpawnEnemy(int wave)
    {
        for (int i = 0; i < _waveEnemy * 2; i++)
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


