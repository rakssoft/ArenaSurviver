using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDropItem : MonoBehaviour
{

    [SerializeField] private List<Item> itemsDrop;

    public void DropItem()
    {
        float totalWeight = 0;
        foreach (var item in itemsDrop)
        {
            totalWeight += item.Weight;
        }

        float randomWeight = Random.Range(0f, totalWeight);

        for (int i = 0; i < itemsDrop.Count; i++)
        {
            randomWeight -= itemsDrop[i].Weight;
            if (randomWeight <= 0)
            {
                itemsDrop[i].Activate(gameObject);
                break;
            }
        }              
    }
}

