using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameInput : IDisposable
  {
    private readonly GameControls _controls = new GameControls();

    public event Action Pressed;
    public event Action Released;
    public event Action<float> Dragged;

    public bool IsPressed => _controls.Gameplay.Press.IsPressed();

    public GameInput()
    {
      _controls.Gameplay.Press.performed += OnPressPerformed;
      _controls.Gameplay.Press.canceled += OnPressCanceled;
      _controls.Gameplay.Drag.performed += OnDragPerformed;
      _controls.Gameplay.Enable();
    }

    public void Dispose()
    {
      _controls.Gameplay.Press.performed -= OnPressPerformed;
      _controls.Gameplay.Press.canceled -= OnPressCanceled;
      _controls.Gameplay.Drag.performed -= OnDragPerformed;
      _controls.Dispose();
    }

    private void OnPressPerformed(InputAction.CallbackContext context) => Pressed?.Invoke();

    private void OnPressCanceled(InputAction.CallbackContext context) => Released?.Invoke();

    private void OnDragPerformed(InputAction.CallbackContext context)
    {
      if (!IsPressed)
        return;

      Dragged?.Invoke(context.ReadValue<Vector2>().x / Screen.width);
    }
  }
}
