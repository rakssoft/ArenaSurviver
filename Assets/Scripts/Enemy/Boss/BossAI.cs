using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private MoveEnemy _moveEnemy;
    [SerializeField] private float _movementSpeed;
    private float _initialRotation; // ����������� ���� �������� �����
    private Vector3 _targetPosition; // �������, ���� ���� ������ ���������
    private bool _isRotated; // ����, �����������, ���������� �� ���� �� 360 ��������
    private float _rotationDelta;
    [SerializeField] float _currentRotation;

    private void Start()
    {
        _initialRotation = transform.eulerAngles.y; // ��������� ����������� ���� �������� �����
    }

    private void Update()
    {
        if (!_isRotated) // ���� ���� ��� �� ���������� �� 360 ��������
        {
            Dash();
        }
        else
        {
            if (Vector3.Distance(transform.position, _targetPosition) > 1f) // ���� ���� �� ������ ����
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _movementSpeed); // ������� ����� � ����

            }
            else
            {
                _initialRotation = _currentRotation; // ��������� ����������� ���� ��������
                _isRotated = false;
            }
        }
    }

    public void Dash()
    {
        _currentRotation = transform.eulerAngles.y; // �������� ������� ���� �������� �����
        _rotationDelta = Mathf.Abs(_currentRotation - _initialRotation);

        if (_rotationDelta >= 350f) // ���� ���� ���������� �� 360 ��������
        {
            _targetPosition = _moveEnemy.GetTarget().transform.position; // ������ ���� ������� �����
            _isRotated = true; // ������������� ����, ��� ���� ���������� �� 360 ��������           
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


