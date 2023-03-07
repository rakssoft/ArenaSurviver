using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/BlackHoleItem")]
public class BlackHoleItem : Item
{

    [SerializeField] private GameObject _blackHolePrefab;
    [SerializeField] private float _speed;
    public override void Activate(GameObject parent)
    {
        GameObject _blackHolePrefGameObject = Instantiate(_blackHolePrefab, parent.transform.position, parent.transform.rotation);
        _blackHolePrefGameObject.AddComponent<ItemBlackHole>();
    }
}
public class ItemBlackHole : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacteristics playerCharacteristics))
        {
            player = playerCharacteristics.gameObject;
            ItemMove[] itemMove = FindObjectsOfType<ItemMove>();
            foreach (ItemMove obj in itemMove)
            {
                obj._target = player.transform;   
            }

            Destroy(gameObject);
        }
    }

}