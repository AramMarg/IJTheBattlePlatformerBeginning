using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private @MoveInputActions _inputActions;

    public event Action<Vector2> MoveClicked;
    public event Action<bool> JumpClicked;
    public event Action<bool> AttackClicked;

    private void Awake()
    {
        _inputActions = new MoveInputActions();
        _inputActions.Enable();
    }

    private void OnEnable()
    {
        _inputActions.PlayerMovement.Move.performed += OnMoved;
        _inputActions.PlayerMovement.Move.canceled += OnMoved;

        _inputActions.PlayerMovement.Jump.performed += OnJumped;
        _inputActions.PlayerMovement.Jump.canceled += OnJumped;

        _inputActions.PlayerMovement.Attack.performed += OnAttaked;
        _inputActions.PlayerMovement.Attack.canceled += OnAttaked;
    }

    private void OnDisable()
    {
        _inputActions.PlayerMovement.Move.performed -= OnMoved;
        _inputActions.PlayerMovement.Move.canceled -= OnMoved;

        _inputActions.PlayerMovement.Jump.performed -= OnJumped;
        _inputActions.PlayerMovement.Jump.canceled -= OnJumped;


        _inputActions.PlayerMovement.Attack.performed -= OnAttaked;
        _inputActions.PlayerMovement.Attack.canceled -= OnAttaked;
    }

    private void OnDestroy()
    {
        _inputActions.Disable();
    }

    private void OnAttaked(InputAction.CallbackContext context)
    {
        bool isAttack = context.ReadValueAsButton();

        AttackClicked?.Invoke(isAttack);
    }

    private void OnJumped(InputAction.CallbackContext context)
    {
        bool isJump = context.ReadValueAsButton();

        JumpClicked?.Invoke(isJump);
    }

    private void OnMoved(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        MoveClicked?.Invoke(direction);
    }
}
