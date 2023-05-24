using UnityEngine;
using UnityEngine.Events;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _pointDamageText;
    private float _maxHealth;
    private float _currentHealth;

    public UnityAction<float> CurrentHPEvent;

    public void RecalHeatlh(float MaxHP)
    {
        _maxHealth = MaxHP;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            EventManager.CurrentCountEnemy?.Invoke(-1);
        }
        EventManager.TakeDamage?.Invoke(_pointDamageText.transform.position, damage.ToString());
        float CurrentHP = _currentHealth / _maxHealth;
        EventManager.TakeDamageBoss?.Invoke(CurrentHP);   // отражается в UI
        CurrentHPEvent?.Invoke(CurrentHP);   // отражается для смерти и фаз
    }
}
