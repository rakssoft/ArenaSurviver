using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Fireball")]
public class FireballAbility : Ability
{
    [SerializeField] private float _baseDamage;
    [SerializeField] private int _basePenetration;
    [SerializeField] private int _currentLevel;
    [SerializeField] private float currentRotationSpeed;
    [SerializeField] private float currentRotationDistance;
    [SerializeField] private GameObject fireballPrefab;

    private float _currentDamage;
    private int _currentPenetration;
  

    public override void Activate(GameObject parent)
    {
        GameObject fireball = Instantiate(fireballPrefab, parent.transform.position + parent.transform.forward * 2, parent.transform.rotation);
        FireballCollisionHandler collisionHandler = fireball.AddComponent<FireballCollisionHandler>();
        collisionHandler.damage = _currentDamage;
        collisionHandler._bulletPenetrationAbility = _currentPenetration;
        Destroy(fireball, Duration);

        FireballRotationHandler rotationHandler = fireball.AddComponent<FireballRotationHandler>();
        rotationHandler.parent = parent.transform;
        rotationHandler.rotationSpeed = currentRotationSpeed;
        rotationHandler.distance = currentRotationDistance;
    }

    public override void LevelUp()
    {
        _currentLevel++;
        float MultimultiplierDamage = 0.2f;
        _currentDamage += _currentDamage * MultimultiplierDamage;
        _currentPenetration = _basePenetration + _currentLevel;
    }
    public override void EnableAbility(float Damage)
    {
        _currentLevel = 1;
        _currentPenetration = _basePenetration;
        _currentDamage = Damage + _baseDamage;
    }
}



public class FireballCollisionHandler : MonoBehaviour
{
    public float damage;
    public int _bulletPenetrationAbility;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthEnemy enemy))
        {
            enemy.TakeDamage(damage);
            _bulletPenetrationAbility--;
            if (_bulletPenetrationAbility <= 0)
            {
                Destroy(gameObject);
                //  gameObject.SetActive(false);
            }

        }
    }
}

public class FireballRotationHandler : MonoBehaviour
{
    public Transform parent;
    public float rotationSpeed;
    public float distance;
    public float offsetX;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = parent.position + new Vector3(distance + offsetX, 0f, 0f);
    }

    private void Update()
    {
        transform.position = parent.position + Quaternion.Euler(0f, rotationSpeed * Time.time, 0f) * new Vector3(distance + offsetX, 0f, 0f);
        transform.rotation = Quaternion.LookRotation(parent.position - transform.position, Vector3.up);
    }


}