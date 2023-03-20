
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/DeadZone")]
public class DeadZoneAbility : Ability
{
    [SerializeField] private int _baseDamage;
    [SerializeField] private int _currentDamage;
    [SerializeField] private GameObject _prefabGamezone;
    [SerializeField] private float _currenttimerActive;
    public override void Activate(GameObject parentObject)
    {
        GameObject deadZone = Instantiate(_prefabGamezone, parentObject.transform.position, Quaternion.identity);
        DeadZoneCollisionHandler collisionHandler = deadZone.AddComponent<DeadZoneCollisionHandler>();
        collisionHandler.damage = _currentDamage;
        Destroy(deadZone, Duration);
    }

    public override void LevelUp()
    {
        int AddDamage = 5;
        _currentDamage += AddDamage;
    }

    private void OnEnable()
    {
        _currentDamage = _baseDamage;
    }
}

public class DeadZoneCollisionHandler : MonoBehaviour
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
