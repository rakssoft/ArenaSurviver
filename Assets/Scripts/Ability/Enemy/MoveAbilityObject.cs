using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbilityObject : MonoBehaviour
{
    private float _forceBullet;
    private float _timerForFalse;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private TakeDamageEnemy _takeDamageEnemy;
    [SerializeField] private GameObject _vfxDamage;

    private float _timer;

    public void StartObject(float damage, Vector3 pos, float force, float timerForFalse)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _takeDamageEnemy = GetComponent<TakeDamageEnemy>();
        _timerForFalse = timerForFalse;
        _forceBullet = force;
        gameObject.transform.position = pos;
        _takeDamageEnemy.RecalDamage(damage);
        _timer = 0;
        _rigidbody.AddForce(transform.forward * _forceBullet, ForceMode.Impulse);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timerForFalse)
        {
            if (_vfxDamage)
            {
                Instantiate(_vfxDamage, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
