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
    public event Action<float> Aimed;

    public bool IsPressed => _controls.Gameplay.Press.IsPressed();
    public float AimOffset => ToAimOffset(_controls.Gameplay.Point.ReadValue<Vector2>());

    public GameInput()
    {
      _controls.Gameplay.Press.performed += OnPressPerformed;
      _controls.Gameplay.Press.canceled += OnPressCanceled;
      _controls.Gameplay.Point.performed += OnPointPerformed;
      _controls.Gameplay.Enable();
    }

    public void Dispose()
    {
      _controls.Gameplay.Press.performed -= OnPressPerformed;
      _controls.Gameplay.Press.canceled -= OnPressCanceled;
      _controls.Gameplay.Point.performed -= OnPointPerformed;
      _controls.Dispose();
    }

    private void OnPressPerformed(InputAction.CallbackContext context)
    {
      Pressed?.Invoke();
      Aimed?.Invoke(AimOffset);
    }

    private void OnPressCanceled(InputAction.CallbackContext context) => Released?.Invoke();

    private void OnPointPerformed(InputAction.CallbackContext context)
    {
      if (!IsPressed)
        return;

      Aimed?.Invoke(ToAimOffset(context.ReadValue<Vector2>()));
    }

    private static float ToAimOffset(Vector2 screenPosition) =>
      Mathf.Clamp(screenPosition.x / Screen.width * 2f - 1f, -1f, 1f);
  }
}
