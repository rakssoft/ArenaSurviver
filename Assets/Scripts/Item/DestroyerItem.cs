
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/DestroyerItem")]
public class DestroyerItem : Item
{
    [SerializeField] private GameObject _destroyerPrefab;
    [SerializeField] private float _radiusDestroy;
    [SerializeField] private float _damage;
    public override void Activate(GameObject parent)
    {
        GameObject _destroyerPrefGameObject = Instantiate(_destroyerPrefab, parent.transform.position, parent.transform.rotation);
        ItemDestroyer collisionHandler = _destroyerPrefGameObject.AddComponent<ItemDestroyer>();
        collisionHandler.ValueCollision = _damage;
        collisionHandler.RadiusCollision = _radiusDestroy;
        
    }
}
public class ItemDestroyer : MonoBehaviour
{
    public float ValueCollision;
    public float RadiusCollision;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacteristics player))
        {
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, RadiusCollision);
            foreach (Collider collider in colliders)
            {

                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(ValueCollision);
                }
            }
            Destroy(gameObject);
        }
    }
}


