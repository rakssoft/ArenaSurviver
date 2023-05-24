using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    private GameObject _target;
    private bool _isMove;


     public void RecalSpeed(float speed)
    {
        _agent.speed = speed;
    }

    private void LateUpdate()
    {
        if (_target)
        {
            MoveTarget();
        }

        if (_agent.velocity.magnitude > 0.1f)
        {
            // Агент движется - включаем анимацию ходьбы
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            // Агент стоит на месте - включаем анимацию покоя
            _animator.SetBool("IsWalking", false);
        }
    }

    public float GetMoveSpeed()
    {
        return _agent.speed;
    }

    public void TargetValue(GameObject target)
    {
        _target = target;
    }

    private void MoveTarget()
    {
        Vector3 targetPosition = _target.transform.position;
        targetPosition.y = transform.position.y; // устанавливаем координату y цели равной координате y игрока
        transform.LookAt(targetPosition);
        _agent.SetDestination(targetPosition);
    }

    public GameObject GetTarget()
    {
        return _target;
    }


}
