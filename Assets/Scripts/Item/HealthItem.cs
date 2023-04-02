using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/HeatlhItem")]
public class HealthItem : Item
{
    [SerializeField] private GameObject _healthPrefab;
    [SerializeField] private float _restoreHealth;
    public override void Activate(GameObject parent)
    {
        GameObject _expaGameobject = Instantiate(_healthPrefab, parent.transform.position, parent.transform.rotation);
        ItemCollisionHeath collisionHandler = _expaGameobject.AddComponent<ItemCollisionHeath>();
        collisionHandler.ValueCollision = _restoreHealth;
    }

}

public class ItemCollisionHeath : MonoBehaviour
{
    public float ValueCollision;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health player))
        {
            player.TakeDamage(ValueCollision);
            Destroy(gameObject);
        }
    }
}

