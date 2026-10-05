using System;

public class InputService : IDisposable
{
    private readonly CharacterInput _inputActions;
    public event Action LeftPressed;
    public event Action RightPressed;
    public event Action Tapped;
    
    public InputService()
    {
        _inputActions = new CharacterInput();

        _inputActions.Character.MoveLeft.performed += OnLeft;
        _inputActions.Character.MoveRight.performed += OnRight;
        _inputActions.Character.Tap.performed += OnTap;
    }

    public void Enable()
    {
        _inputActions.Enable();
    }
    public void Disable()
    {
        _inputActions.Disable();
    }
    private void OnLeft(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        LeftPressed?.Invoke();
    }
    private void OnRight(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        RightPressed?.Invoke();
    }
    private void OnTap(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Tapped?.Invoke();
    }
    public void Dispose()
    {
        _inputActions.Character.MoveLeft.performed -= OnLeft;
        _inputActions.Character.MoveRight.performed -= OnRight;
        _inputActions.Character.Tap.performed -= OnTap;
        _inputActions.Disable();
    }
}
