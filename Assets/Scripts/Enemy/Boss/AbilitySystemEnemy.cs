using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemEnemy : MonoBehaviour
{
    [SerializeField] private CharacteristicsEnemy _characteristicsEnemy;
    [SerializeField] private MoveEnemy _moveEnemy;
    [SerializeField] private AbilityEnemy[] _abilities;
    [SerializeField] private GameObject _pointsAttack;
    [SerializeField] private float _radiusAttack;
    [SerializeField] private float _timerAttack;
    [SerializeField] private float _damage;
    [SerializeField] private Animator _animator;
    public float _timer;
    public float _timerPreparation;
    private GameObject _target;
    private float _bufferSpeed;
    private bool _isPreparation;


    private float closestDistance;
    public AbilityEnemy currentAbility;
    public float distanceToTarget;

    private void Start()
    {
        _isPreparation = false;
           _timer = _timerAttack;
        currentAbility = null;
        closestDistance = float.MaxValue;
        _damage = _characteristicsEnemy.GetBaseDamage();
    }

    private void LateUpdate()
    {       
        _timer -= Time.deltaTime;
        if (_timer <= 0 && currentAbility == null)
        {
            // Iterate through all abilities to find the closest one
            foreach (AbilityEnemy ability in _abilities)
            {
                if (ability.DistanceAttack >= GetDistanceChekTarget())
                {
                    distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                    // Check if this ability is closer than the previous closest one
                    if (Mathf.Abs(distanceToTarget - ability.DistanceAttack) < Mathf.Abs(closestDistance - ability.DistanceAttack))
                    {
                        closestDistance = distanceToTarget;
                        currentAbility = ability;
                        _timerPreparation = currentAbility.ÑhargingTimer;
                    }
                }
            }  
        }
        // Activate the selected ability
        if (currentAbility != null)
        {
            PreparationAbilityy(currentAbility);
            _timerPreparation -= Time.deltaTime;
            if (_timerPreparation <= 0)
            {
                _moveEnemy.RecalSpeed(_bufferSpeed);
                _timer = _timerAttack;
                _pointsAttack.transform.LookAt(_target.transform.position);
                currentAbility.Activate(_pointsAttack, _damage);
                if (_animator)
                {
                    _animator.SetTrigger("IsAttack");
                }
                currentAbility.EnableAbility();
                currentAbility = null;
                closestDistance = float.MaxValue;
                _isPreparation = false;
            }
        }
    }

    public void PreparationAbilityy(AbilityEnemy ability)
    {
        if(_isPreparation == false)
        {
            _bufferSpeed = _moveEnemy.GetMoveSpeed();
            _isPreparation = true;
            _moveEnemy.RecalSpeed(0);
            ability.PreparationAbilityy(gameObject);
        }
    }


    public void TargetValue(GameObject target)
    {
        _target = target;
    }  
    private float GetDistanceChekTarget()
    {
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, _target.transform.position);
        return distanceToTarget;
    }
    public void RecalDamage(float dmg)
    {
        _damage += dmg;
    }

    public void RecalTimerAttack(float timer)
    {
        _timerAttack = _timerAttack * timer;
    }

    private int GetChooseAttack()
    {
        int random = Random.RandomRange(0, _abilities.Length);
        return random;
    }

}
