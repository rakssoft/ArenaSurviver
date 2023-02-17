
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _rateTime;
    private  float _timer;


    private void Start()
    {
        _timer = _rateTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerStats>(out PlayerStats _player))
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                print("uron");
                _timer = _rateTime;
                _player.TakeDamage(_damage);
            }
        }
    }
}
