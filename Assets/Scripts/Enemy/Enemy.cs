using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private List<Item> itemsDrop;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource shootGun;
    private float timer;
    private float _health = 1;
    private GameObject target;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerMove>().gameObject;
        if (target)
        {
            MoveTarget();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.5)
        {
            timer = 0;
            if (target)
            {
                MoveTarget();
            }
        }
    }

    private void MoveTarget()
    {
        transform.LookAt(target.transform.position);
        agent.SetDestination(target.transform.position);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
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

            EventManager.TakeDamage?.Invoke(gameObject.transform.position, damage.ToString());
            EventManager.CurrentCountEnemy?.Invoke(-1);

            Destroy(gameObject);
        }
    }
}
