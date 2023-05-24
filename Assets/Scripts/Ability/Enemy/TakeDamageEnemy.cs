
using UnityEngine;

public class TakeDamageEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _vfxDamage;
    private float _damage;
    private float _knockbackForce = 0;


    public void KnockbackForce(float force)
    {
        _knockbackForce = force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health player))
        {
            player.TakeDamage(_damage);
            if (other.TryGetComponent(out CharacterMove characterMove) && _knockbackForce > 5)
            {
                characterMove.ShockEffect();
            }
            if (_vfxDamage)
            {
                Instantiate(_vfxDamage, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        } 
        
        if (other.TryGetComponent(out Wall wall))
        {          
            if (_vfxDamage)
            {
                Instantiate(_vfxDamage, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    public void RecalDamage(float dmg)
    {
        _damage = dmg;
    }


}
