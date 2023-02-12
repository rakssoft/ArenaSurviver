using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject _spawnFire;
    [SerializeField] private float _radiusSphere;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _parentBullets;
    [SerializeField] private Bullet[] _poolBullets;
    [SerializeField] private float _damageBullets;
    [SerializeField] private int _bulletPenetration;
    private int currentIndexBulletInPool;
    private float _rateFire;

    private void Start()
    {
        _rateFire = _playerStats.FireRate;
        currentIndexBulletInPool = _poolBullets.Length;
        for (int i = 0; i < _poolBullets.Length; i++)
        {
            _poolBullets[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _rateFire -= Time.deltaTime;
        if (_rateFire <= 0)
        {
            _rateFire = _playerStats.FireRate;
            NearestEnemy();
        }
    }
    /// <summary>
    /// поиск ближайшего врага
    /// при нахождение стрельба из основного оружия
    /// </summary>
    private void NearestEnemy()
    {
        float shortestDistance = Mathf.Infinity;                                          // кратчайшее растояние
        GameObject nearestEnemy = null;                                                  // ближайший ноль    
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusSphere);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.TryGetComponent(out Enemy enemy))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);         // поверяет всех на ближайшее растояние
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy.gameObject;
                }
            }
        }
        if (nearestEnemy)
        {
            Shoot(nearestEnemy.transform.position);
        }
    }


    /// <summary>
    /// стрельба вытаскивание пули из пула
    /// </summary>
    /// <param name="enemy"></param>
    private void Shoot(Vector3 enemy)
    {  
        if (currentIndexBulletInPool == 0)
        {
            currentIndexBulletInPool = _poolBullets.Length;
        }
        currentIndexBulletInPool--;
        gameObject.transform.LookAt(enemy);
       
        _poolBullets[currentIndexBulletInPool].gameObject.transform.position = _spawnFire.transform.position;
        _poolBullets[currentIndexBulletInPool].gameObject.transform.rotation = _spawnFire.transform.rotation;
        _poolBullets[currentIndexBulletInPool].gameObject.SetActive(true);
        _poolBullets[currentIndexBulletInPool].DamageBullet = _damageBullets;
        _poolBullets[currentIndexBulletInPool].BulletPenetrationAbility = _bulletPenetration;
        _poolBullets[currentIndexBulletInPool].StartBullet();


        //    GameObject bullet = Instantiate(_bullet, _spawnFire.transform.position, Quaternion.identity, _parentBullets);
        //  bullet = _spawnFire.transform.rotation;
        //     soundManger._fireTower.Play();
        //   Instantiate(vfx_prefabBullet, spawnBulet.transform.position, spawnBulet.rotation);

    }

}

