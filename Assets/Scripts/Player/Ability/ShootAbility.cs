
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Shoot")]
public class ShootAbility : Ability
{
    [SerializeField] private float _radiusSphere;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _baseDamage;
    [SerializeField] private int _basePenetration;
    [SerializeField] private int _currentLevel;
    
    private int _currentPenetration;
    private float _currentDamage;


    private void OnEnable()
    {
        _currentLevel = Level;
        _currentPenetration = _basePenetration;
        _currentDamage = _baseDamage;
    }
    public override void Activate(GameObject parent)
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        Collider[] colliders = Physics.OverlapSphere(parent.transform.position, _radiusSphere);
        foreach (Collider nearbyObject in colliders)
        {
           
            if (nearbyObject.TryGetComponent(out Enemy enemy))
            {
                float distanceToEnemy = Vector3.Distance(parent.transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy.gameObject;
                }
            }
        }
        if (nearestEnemy)
        {
            Shoot(nearestEnemy, parent);
        }
    }

    public override void LevelUp()
    {
        _currentLevel++;
        _currentDamage = _baseDamage + (_currentLevel * 10);
        _currentPenetration = _basePenetration + _currentLevel;
    }
    private void Shoot(GameObject enemy, GameObject parent)
    {
        parent.transform.LookAt(enemy.transform.position);
        parent.transform.eulerAngles = new Vector3(0, parent.transform.eulerAngles.y, 0);

        GameObject bull = Instantiate(_bulletPrefab, parent.transform.position, Quaternion.identity);
        bull.transform.rotation = parent.transform.rotation;
        bull.GetComponent<Bullet>().StartBullet(_currentPenetration, _currentDamage, parent.transform.position, 50);

    }
}
