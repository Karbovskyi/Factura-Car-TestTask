using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "TurretConfig", menuName = "Factura Car/Turret Config")]
  public class TurretConfig : ScriptableObject, ITurretConfig
  {
    [SerializeField, Range(0f, 180f)] private float _maxAngle;
    [SerializeField, Min(1f)] private float _aimExponent;
    [SerializeField, Min(0f)] private float _rotationSpeed;
    [SerializeField, Min(0.1f)] private float _fireRate;

    public float MaxAngle => _maxAngle;
    public float AimExponent => _aimExponent;
    public float RotationSpeed => _rotationSpeed;
    public float FireRate => _fireRate;
  }
}
