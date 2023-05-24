using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterCharacteristics _characterCharacteristics;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _vfxStan;
    private CharacterController _controller;
    private float _movementSpeed;
    private bool _isShockEffect;
    private bool _isMoving;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _isShockEffect = false;
        _isMoving = false;
        _vfxStan.SetActive(false);
    }

    private void FixedUpdate()
    {
        var targetVector = new Vector3(_playerController.InputVector.x, 0f, _playerController.InputVector.y);
        MoveTowardTarget(targetVector);
        RotateTowardMovementVector(targetVector);

        if (_isMoving)
        {
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }
    }

    private void MoveTowardTarget(Vector3 targetVector)
    {
        if (_isShockEffect)
        {
            _movementSpeed = _characterCharacteristics.Speed * 0.5f;
        }
        else
        {
            _movementSpeed = _characterCharacteristics.Speed;
        }

        if (targetVector.magnitude != 0)
        {
            _isMoving = true;
            var movementVector = targetVector.normalized * _movementSpeed;
            _controller.Move(movementVector * Time.deltaTime);
        }
        else
        {
            _isMoving = false;
        }
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0)
        {
            return;
        }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed);
    }

    public void ShockEffect()
    {
        _isShockEffect = true;
        _vfxStan.SetActive(true);
        StartCoroutine(ShockEffectTimer());
    }

    IEnumerator ShockEffectTimer()
    {
        yield return new WaitForSeconds(3f);
        _isShockEffect = false;
        _vfxStan.SetActive(false);
    }
}
