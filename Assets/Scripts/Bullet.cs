
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _forceBullet;
    [SerializeField] private float _timerForFalse;
    [SerializeField] private Rigidbody _rigidbody;
    private float _timer;
    private int _bulletPenetrationAbility;
    private float _damageBullet;
    public void StartBullet(int penetration, float damage, Vector3 pos, float force)
    {
        _forceBullet = force;
        gameObject.transform.position = pos;
        _bulletPenetrationAbility = penetration;
        _damageBullet = damage;
        _timer = 0;
        _rigidbody.AddForce(transform.forward * _forceBullet, ForceMode.Impulse);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _timerForFalse)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damageBullet);
            _bulletPenetrationAbility--;
            if(_bulletPenetrationAbility <= 0)
            {
                gameObject.SetActive(false);
            }

        }
    }

}
