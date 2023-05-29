
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
        else if (!nearestEnemy)
        {
            Shoot(parent, parent);
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
        cooldownTime = 0;
    }
    public override int GetCurrentStatsAbility()
    {
        return _currentLevel;
    }


    private void Shoot(GameObject enemy, GameObject parent)
    {
    //    Quaternion parentRotation = parent.transform.rotation;
        // Сохраняем текущий поворот родительского объекта

        parent.transform.LookAt(enemy.transform.position);
        // Поворачиваем родительский объект в сторону позиции врага
        // Это изменяет поворот родительского объекта так, чтобы он смотрел на позицию врага

        parent.transform.eulerAngles = new Vector3(0, parent.transform.eulerAngles.y, 0);
        // Задаем углы поворота родительского объекта, обнуляя наклон и сохраняя только поворот вокруг оси Y

        GameObject bull = Instantiate(_bulletPrefab, parent.transform.position, Quaternion.identity);
        // Создаем экземпляр пули (или объекта-проектайла) в позиции родительского объекта
        // Quaternion.identity означает, что мы не хотим поворачивать пулю

        bull.transform.rotation = parent.transform.rotation;
        // Устанавливаем поворот пули таким же, как и поворот родительского объекта

      //  parent.transform.rotation = parentRotation;
        // Восстанавливаем исходный поворот родительского объекта

        bull.GetComponent<Bullet>().StartBullet(_currentPenetration, _currentDamage, parent.transform.position, 50);
        // Вызываем метод StartBullet() на компоненте Bullet (или любом другом компоненте, реализующем метод),
        // передавая ему данные о проникновении, уроне, позиции родительского объекта и скорости пули
    }





}
