
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/ExpaItem")]
public class ExpaItem : Item
{
    [SerializeField] private GameObject _expaPrefab;
    [SerializeField] private int _amountExperience;
    public override void Activate(GameObject parent)
    {
        GameObject _expaGameobject = Instantiate(_expaPrefab, parent.transform.position, parent.transform.rotation);
        ItemCollisionHandler collisionHandler = _expaGameobject.AddComponent<ItemCollisionHandler>();
        collisionHandler.ValueCollision = _amountExperience;
    }
}
public class ItemCollisionHandler : MonoBehaviour
{
    public float ValueCollision;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacteristics player))
        {
            player.TakeExperience(ValueCollision);
            Destroy(gameObject);
        }
    }
}

