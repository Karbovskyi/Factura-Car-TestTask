using UnityEngine;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Turret : MonoBehaviour
  {
    private ITurretConfig _config;
    private float _angle;
    private float _targetAngle;

    [Inject]
    public void Construct(ITurretConfig config)
    {
      _config = config;
    }

    public void Aim(float screenWidths)
    {
      float angle = _targetAngle + screenWidths * _config.DegreesPerScreenWidth;
      _targetAngle = Mathf.Clamp(angle, -_config.MaxAngle, _config.MaxAngle);
    }

    public void ResetAim()
    {
      _angle = 0f;
      _targetAngle = 0f;
      ApplyAngle();
    }

    private void Update()
    {
      _angle = Mathf.MoveTowards(_angle, _targetAngle, _config.RotationSpeed * Time.deltaTime);
      ApplyAngle();
    }

    private void ApplyAngle() => transform.localRotation = Quaternion.Euler(0f, _angle, 0f);
  }
}
