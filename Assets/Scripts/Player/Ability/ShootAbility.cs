
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability", order = 51)]
public class ShootAbility : Ability
{
    [SerializeField] private float _radiusSphere;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _damageBullets;
    [SerializeField] private int _bulletPenetration;
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
    private void Shoot(GameObject enemy, GameObject parent)
    {
        parent.transform.LookAt(enemy.transform.position);
        parent.transform.eulerAngles = new Vector3(0, parent.transform.eulerAngles.y, 0);

        GameObject bull = Instantiate(_bullet, parent.transform.position, Quaternion.identity);
        bull.transform.rotation = parent.transform.rotation;
        bull.GetComponent<Bullet>().StartBullet(_bulletPenetration, _damageBullets, parent.transform.position, 50);

    }
}
