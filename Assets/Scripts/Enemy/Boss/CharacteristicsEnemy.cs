
using System.Collections;
using UnityEngine;

public class CharacteristicsEnemy : MonoBehaviour
{
    [Header("Characteristics")]

    public float MaxHealth;
    public float Speed;
    public float BaseDamage;
    public bool Activated = false;
    [SerializeField] private bool _isBoss;

    [Header("Components")]

    [SerializeField] private HealthEnemy _healthEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private MoveEnemy _moveEnemy;
    [SerializeField] private AbilitySystemEnemy _abilitySystemEnemy;
    [SerializeField] private EnemyDropItem _enemyDropItem;

    private GameObject _target;
    private bool _isPhaseChanged = false;


    private void OnEnable()
    {
        _healthEnemy.CurrentHPEvent += RecalHeatlh; 
    }
    private void OnDisable()
    {
        _healthEnemy.CurrentHPEvent -= RecalHeatlh;
    }
    private void Start()
    {       
        _target = FindObjectOfType<CharacterMove>().gameObject;
        _moveEnemy.TargetValue(_target);
        _abilitySystemEnemy.TargetValue(_target);
        _healthEnemy.RecalHeatlh(MaxHealth);
        _moveEnemy.RecalSpeed(Speed);
    }

    public void ActivateEnemy(GameObject target)
    {
        _target = target;
    }
    private void RecalHeatlh(float currenHealth)
    {
        if (_isBoss && currenHealth == 0)
        {
            _animator.SetBool("IsDead", true);
            _moveEnemy.RecalSpeed(0);
            _moveEnemy.TargetValue(null);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            _abilitySystemEnemy.enabled = false;
            StartCoroutine(TimerBossWin());
        }
        if (!_isBoss && currenHealth <= 0)
        {
            _animator.SetBool("IsDead", true);
            _moveEnemy.RecalSpeed(0);
            _moveEnemy.TargetValue(null);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            _abilitySystemEnemy.enabled = false;
            StartCoroutine(TimerDeathEnemy());

        }

        if (_isBoss && currenHealth < 0.75 && currenHealth > 0.3 && !_isPhaseChanged) // проверяем, не изменилась ли фаза ранее
        {
            _animator.SetTrigger("IsChangePhase");
            _moveEnemy.RecalSpeed(Speed * 1.5f);
            _abilitySystemEnemy.RecalDamage(BaseDamage * 0.7f);
            _abilitySystemEnemy.RecalTimerAttack(0.9f);
            _isPhaseChanged = true;
        }
        else if (_isBoss && currenHealth < 0.3 && _isPhaseChanged) // проверяем, изменилась ли фаза ранее и текущее значение здоровья
        { 
            _animator.SetTrigger("IsChangePhase");
            _moveEnemy.RecalSpeed(Speed * 3f);
            _abilitySystemEnemy.RecalDamage(BaseDamage * 0.9f);
            _abilitySystemEnemy.RecalTimerAttack(0.6f);
            _isPhaseChanged = false;
        }
    }
    public void IsAoeAttack()
    {
        _animator.SetTrigger("IsAoeAttack");
    }

    public float GetBaseDamage()
    {
        return BaseDamage;
    }

   private IEnumerator TimerBossWin()
    {
        yield return new WaitForSeconds(5);
        EventManager.BatttleIsWon?.Invoke(true);
        Destroy(gameObject);
    }
    private IEnumerator TimerDeathEnemy()
    {
        yield return new WaitForSeconds(1.5f);

        if (_enemyDropItem)
        {
            _enemyDropItem.DropItem();
        }

        gameObject.SetActive(false);
    }
}
