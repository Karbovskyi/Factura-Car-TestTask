using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameInput : IDisposable
  {
    private readonly GameControls _controls = new GameControls();

    public event Action Tapped;
    public event Action<float> Dragged;

    public GameInput()
    {
      _controls.Gameplay.Press.performed += OnPressPerformed;
      _controls.Gameplay.Drag.performed += OnDragPerformed;
      _controls.Gameplay.Enable();
    }

    public void Dispose()
    {
      _controls.Gameplay.Press.performed -= OnPressPerformed;
      _controls.Gameplay.Drag.performed -= OnDragPerformed;
      _controls.Dispose();
    }

    private void OnPressPerformed(InputAction.CallbackContext context) => Tapped?.Invoke();

    private void OnDragPerformed(InputAction.CallbackContext context)
    {
      if (!_controls.Gameplay.Press.IsPressed())
        return;

      Dragged?.Invoke(context.ReadValue<Vector2>().x / Screen.width);
    }
  }
}
