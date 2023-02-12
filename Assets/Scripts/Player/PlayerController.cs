using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _inputActions;


    public Vector2 InputVector { get; private set; }


    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Awake()
    {
        _inputActions = new PlayerInput();
    }


    private void Update()
    {
        InputVector = _inputActions.Player.Move.ReadValue<Vector2>();
    }

}
