
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/ShockWave")]
public class ShockWaveAbility : Ability
{
    [SerializeField] private float _baseDamage;
    [SerializeField] private GameObject _shockWavePrefab;
    [SerializeField] private int _currentLevel;

    private float _currentDamage;


    public override void Activate(GameObject parent)
    {
        GameObject shockWave = Instantiate(_shockWavePrefab, parent.transform.position, Quaternion.identity);
        ShockWaveCollisionHandler collisionHandler = shockWave.AddComponent<ShockWaveCollisionHandler>();
        collisionHandler.damage = _currentDamage;
        Destroy(shockWave, Duration);

        ShockWaveposition rotationHandler = shockWave.AddComponent<ShockWaveposition>();
        rotationHandler.parent = parent.transform;
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
}



public class ShockWaveCollisionHandler : MonoBehaviour
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

public class ShockWaveposition : MonoBehaviour
{
    public Transform parent;

    private void Update()
    {
        transform.position = parent.position;
    }


}
