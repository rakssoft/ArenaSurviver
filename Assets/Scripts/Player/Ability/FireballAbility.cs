using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Fireball")]
public class FireballAbility : Ability
{
    [SerializeField] private int baseDamage;
    [SerializeField] private int basePenetrationAbility;
    [SerializeField] private int _currentLevelAbility;
    [SerializeField] private float currentRotationSpeed;
    [SerializeField] private float currentRotationDistance;
    [SerializeField] private GameObject fireballPrefab;
  
    private int currentDamage;
    private int currentPenetrationAbility;
  

    public override void Activate(GameObject parent)
    {
        GameObject fireball = Instantiate(fireballPrefab, parent.transform.position + parent.transform.forward * 2, parent.transform.rotation);
        FireballCollisionHandler collisionHandler = fireball.AddComponent<FireballCollisionHandler>();
        collisionHandler.damage = currentDamage;
        collisionHandler._bulletPenetrationAbility = currentPenetrationAbility;
        Destroy(fireball, Duration);

        FireballRotationHandler rotationHandler = fireball.AddComponent<FireballRotationHandler>();
        rotationHandler.parent = parent.transform;
        rotationHandler.rotationSpeed = currentRotationSpeed;
        rotationHandler.distance = currentRotationDistance;
    }

    public override void LevelUp()
    {
        _currentLevelAbility++;
        currentDamage = baseDamage + (_currentLevelAbility * 10);
        currentPenetrationAbility = basePenetrationAbility + _currentLevelAbility;

    }

    private void OnEnable()
    {
        _currentLevelAbility = Level;
        currentDamage = baseDamage + (Level * 10);
        currentPenetrationAbility = basePenetrationAbility + Level;
    }
}



public class FireballCollisionHandler : MonoBehaviour
{
    public int damage;
    public int _bulletPenetrationAbility;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
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