using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, IInGameActions, ILocalSecPlayerActions
{
    public event Action<float> OnUpdownInput;
    public event Action<float> OnSecUpdownInput;
    public event Action OnEnterInput;
    public event Action OnSecEnterInput;

    private Controls _controls;
    public Controls Controls => _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.InGame.SetCallbacks(this);
            _controls.LocalSecPlayer.SetCallbacks(this);
        }

        _controls.InGame.Enable();
    }
    private void OnDisable()
    {
        _controls.InGame.Disable(); 
        _controls.LocalSecPlayer.Disable();
    }

    public void ReleaseAllEvents()
    {
        OnUpdownInput = null;
        OnEnterInput = null;
    }
    public void ReleaseAllSecEvents()
    {
        OnSecUpdownInput = null;
        OnSecEnterInput = null;
    }

    public void EnableLocalPlay()
    {
        _controls.LocalSecPlayer.Enable();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnUpdownInput?.Invoke(context.ReadValue<float>());
        }
        if (context.canceled)
        {
            OnUpdownInput?.Invoke(0);
        }
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnEnterInput?.Invoke();
        }
    }

    public void OnSecUpDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSecUpdownInput?.Invoke(context.ReadValue<float>());
        }
        if (context.canceled)
        {
            OnSecUpdownInput?.Invoke(0);
        }
    }

    public void OnSecEnter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSecEnterInput?.Invoke();
        }
    }
}
