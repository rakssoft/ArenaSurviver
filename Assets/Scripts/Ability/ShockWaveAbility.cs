
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/ShockWave")]
public class ShockWaveAbility : Ability
{
    [SerializeField] private int _baseDamage;
    [SerializeField] private GameObject _shockWavePrefab;

    private int _currentDamage;


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
    }

    private void OnEnable()
    {
        _currentDamage = _baseDamage;

    }
}



public class ShockWaveCollisionHandler : MonoBehaviour
{
    public int damage;
    private float damageInterval = 1f; // интервал между атаками в секундах
    private float timer = 0f; // время с момента последней атаки

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
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
