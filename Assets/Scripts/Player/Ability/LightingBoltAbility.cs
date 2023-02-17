
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "LightBolt", order = 51)]
public class LightingBoltAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        int _damage = 10;
        RaycastHit hit;
        if (Physics.Raycast(parent.transform.position, parent.transform.forward, out hit, 10))
        {
            Debug.DrawRay(parent.transform.position, parent.transform.forward * hit.distance, Color.yellow);
            
            if (hit.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);

            }

        }
    }
}
