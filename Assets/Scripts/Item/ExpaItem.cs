
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
        collisionHandler._amountExperience = _amountExperience;
    }
}
public class ItemCollisionHandler : MonoBehaviour
{
    public int _amountExperience;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.TakeExperience(_amountExperience);
            Destroy(gameObject);
        }
    }
}
