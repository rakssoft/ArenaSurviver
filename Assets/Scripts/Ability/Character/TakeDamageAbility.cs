
using UnityEngine;

public class TakeDamageAbility : MonoBehaviour
{

    public int damage;
    public int _bulletPenetrationAbility;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthEnemy enemy))
        {
            enemy.TakeDamage(damage);
            _bulletPenetrationAbility--;
            if (_bulletPenetrationAbility <= 0)
            {
                Destroy(gameObject);
                //  gameObject.SetActive(false);
            }

        }
    }
}
