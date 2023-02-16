using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{

    public GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource shootGun;
    private float timer;
    private float _health = 1;

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
        if(timer >= 1.5)
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
        if(_health <= 0)
        {
            EventManager.TakeDamage?.Invoke(gameObject.transform.position, damage.ToString());
            EventManager.CurrentCountEnemy?.Invoke(-1);
            EventManager.ExperienceDropEnemy?.Invoke(1);
            Destroy(gameObject);
          
        }
    }
}

