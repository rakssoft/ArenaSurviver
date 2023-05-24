using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private MoveEnemy _moveEnemy;
    [SerializeField] private float _movementSpeed;
    private float _initialRotation; // изначальный угол поворота босса
    private Vector3 _targetPosition; // позиция, куда босс должен двигаться
    private bool _isRotated; // флаг, указывающий, повернулся ли босс на 360 градусов
    private float _rotationDelta;
    [SerializeField] float _currentRotation;

    private void Start()
    {
        _initialRotation = transform.eulerAngles.y; // сохраняем изначальный угол поворота босса
    }

    private void Update()
    {
        if (!_isRotated) // если босс еще не повернулся на 360 градусов
        {
            Dash();
        }
        else
        {
            if (Vector3.Distance(transform.position, _targetPosition) > 1f) // если босс не достиг цели
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _movementSpeed); // двигаем босса к цели

            }
            else
            {
                _initialRotation = _currentRotation; // обновляем изначальный угол поворота
                _isRotated = false;
            }
        }
    }

    public void Dash()
    {
        _currentRotation = transform.eulerAngles.y; // получаем текущий угол поворота босса
        _rotationDelta = Mathf.Abs(_currentRotation - _initialRotation);

        if (_rotationDelta >= 350f) // если босс повернулся на 360 градусов
        {
            _targetPosition = _moveEnemy.GetTarget().transform.position; // задаем цель впереди босса
            _isRotated = true; // устанавливаем флаг, что босс повернулся на 360 градусов           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterMove character))
        {
            character.ShockEffect();
        }
    }
}


