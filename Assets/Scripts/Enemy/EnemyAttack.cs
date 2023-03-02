
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _rateOfFire;
    private  float _timer;


    private void Start()
    {
        _timer = _rateOfFire;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerStats>(out PlayerStats _player))
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                _timer = _rateOfFire;
                _player.TakeDamage(_damage);
            }
        }
    }
}
