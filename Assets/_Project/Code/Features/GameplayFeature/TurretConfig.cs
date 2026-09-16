using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "TurretConfig", menuName = "Factura Car/Turret Config")]
  public class TurretConfig : ScriptableObject, ITurretConfig
  {
    [SerializeField, Range(0f, 180f)] private float _maxAngle;
    [SerializeField, Min(0f)] private float _degreesPerScreenWidth;
    [SerializeField, Min(0f)] private float _rotationSpeed;

    public float MaxAngle => _maxAngle;
    public float DegreesPerScreenWidth => _degreesPerScreenWidth;
    public float RotationSpeed => _rotationSpeed;
  }
}
