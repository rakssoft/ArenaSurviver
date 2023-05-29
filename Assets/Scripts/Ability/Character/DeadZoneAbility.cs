
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/DeadZone")]
public class DeadZoneAbility : Ability
{
    [SerializeField] private float _baseDamage;
    [SerializeField] private int _currentLevel;
    [SerializeField] private GameObject _prefabGamezone;
    [SerializeField] private float _currenttimerActive;

    private float _currentDamage;

    private void OnEnable()
    {
        _currentLevel = Level;
    }
    public override void Activate(GameObject parentObject)
    {
        GameObject deadZone = Instantiate(_prefabGamezone, parentObject.transform.position, Quaternion.identity);
        DeadZoneCollisionHandler collisionHandler = deadZone.AddComponent<DeadZoneCollisionHandler>();
        collisionHandler.damage = _currentDamage;
        Destroy(deadZone, Duration);
    }

    public override void LevelUp()
    {
        _currentLevel++;
        float MultimultiplierDamage = 0.2f;
        _currentDamage += _currentDamage * MultimultiplierDamage;
    }


    public override void EnableAbility(float Damage)
    {
        _currentLevel = 1;
        _currentDamage = Damage + _baseDamage;
    }

    public override int GetCurrentStatsAbility()
    {
        return _currentLevel;
    }
}

public class DeadZoneCollisionHandler : MonoBehaviour
{
    public float damage;
    private float damageInterval = 1f; // интервал между атаками в секундах
    private float timer = 0f; // время с момента последней атаки

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthEnemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    } 
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out HealthEnemy enemy))
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                enemy.TakeDamage(damage);
                timer = damageInterval;
            }
        }
    }

}
