using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerStats _playerStats;
    private float _movementSpeed;
    private float _rotationSpeed;


    private void FixedUpdate()
    {
        var targetVector = new Vector3(_playerController.InputVector.x, 0, _playerController.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);
        RotateTowardMovementVector(movementVector);
        if (movementVector.x != 0 || movementVector.z != 0)
        {
            _animator.SetBool("Run", true);
        }
        else if (movementVector.x == 0 || movementVector.z == 0)
        {
            _animator.SetBool("Run", false);
        }
    }



    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        _movementSpeed = _playerStats.SpeedMove;
        float speed = _movementSpeed * Time.deltaTime;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        _rotationSpeed = _playerStats.SpeedRotations;
        if (movementDirection.magnitude == 0)
        {
            return;
        }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed);
    }
}
