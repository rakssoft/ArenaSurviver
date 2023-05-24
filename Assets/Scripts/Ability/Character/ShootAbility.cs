
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

    public override void Activate(GameObject parent)
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        Collider[] colliders = Physics.OverlapSphere(parent.transform.position, _radiusSphere);
        foreach (Collider nearbyObject in colliders)
        {

            if (nearbyObject.TryGetComponent(out HealthEnemy enemy))
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
        float MultimultiplierDamage = 0.2f;
        _currentDamage += _currentDamage * MultimultiplierDamage;
        _currentPenetration = _basePenetration + _currentLevel;
    }

    public override void EnableAbility(float Damage)
    {
        _currentLevel = 1;
        _currentPenetration = _basePenetration;
        _currentDamage = Damage + _baseDamage;
      
    }


    private void Shoot(GameObject enemy, GameObject parent)
    {
        Quaternion parentRotation = parent.transform.rotation; // ��������� ������� �������
        parent.transform.LookAt(enemy.transform.position); // ������������ ������������ ������ � ������� Enemy
        parent.transform.eulerAngles = new Vector3(0, parent.transform.eulerAngles.y, 0); // ���������� ������� �� ��� x � z

        GameObject bull = Instantiate(_bulletPrefab, parent.transform.position, Quaternion.identity);
        bull.transform.rotation = parent.transform.rotation; // ������������� ������� ��� ����

        // ��������������� ����������� ������� ��� ������������� �������
        parent.transform.rotation = parentRotation;

        bull.GetComponent<Bullet>().StartBullet(_currentPenetration, _currentDamage, parent.transform.position, 50);
    }

}
