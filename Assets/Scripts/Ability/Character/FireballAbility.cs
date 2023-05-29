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


    private void OnEnable()
    {
        _currentLevel = Level;
    }
    public override void Activate(GameObject parent)
    {
        GameObject fireball = Instantiate(fireballPrefab, parent.transform.position + parent.transform.forward * 2, parent.transform.rotation);
        FireballCollisionHandler collisionHandler = fireball.AddComponent<FireballCollisionHandler>();
        collisionHandler.Damage = _currentDamage;
        collisionHandler.BulletPenetrationAbility = _currentPenetration;
        collisionHandler.Ability = this;
        Destroy(fireball, Duration);

        FireballRotationHandler rotationHandler = fireball.AddComponent<FireballRotationHandler>();
        rotationHandler.Parent = parent.transform;
        rotationHandler.RotationSpeed = currentRotationSpeed;
        rotationHandler.Ddistance = currentRotationDistance;
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
      
        _currentPenetration = _basePenetration;
        _currentDamage = Damage + _baseDamage;
    }

    public override int GetCurrentStatsAbility()
    {
        return _currentLevel;
    }
}



public class FireballCollisionHandler : MonoBehaviour
{
    public float Damage;
    public int BulletPenetrationAbility;
    public Ability Ability;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthEnemy enemy))
        {
            enemy.TakeDamage(Damage);
            BulletPenetrationAbility--;
            if (BulletPenetrationAbility <= 0)
            {
                Ability.activeTime = 0;
                Destroy(gameObject);
                //  gameObject.SetActive(false);
            }

        }
    }
}

public class FireballRotationHandler : MonoBehaviour
{
    public Transform Parent;
    public float RotationSpeed;
    public float Ddistance;
    public float OffsetX;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = Parent.position + new Vector3(Ddistance + OffsetX, 0f, 0f);
    }

    private void Update()
    {
        transform.position = Parent.position + Quaternion.Euler(0f, RotationSpeed * Time.time, 0f) * new Vector3(Ddistance + OffsetX, 0f, 0f);
        transform.rotation = Quaternion.LookRotation(Parent.position - transform.position, Vector3.up);
    }


}