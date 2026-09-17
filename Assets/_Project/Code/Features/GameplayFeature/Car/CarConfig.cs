using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "CarConfig", menuName = "Factura Car/Car Config")]
  public class CarConfig : ScriptableObject, ICarConfig
  {
    [SerializeField] private Car _prefab;
    [SerializeField, Min(1)] private int _maxHealth;
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(0f)] private float _acceleration;
    [SerializeField, Min(0f)] private float _braking;
    [SerializeField, Min(0f)] private float _launchDelay;
    [SerializeField, Min(0f)] private float _weaveAmplitude;
    [SerializeField, Min(0.1f)] private float _weaveWavelength;
    [SerializeField, Min(0.1f)] private float _weaveRampDistance;

    public Car Prefab => _prefab;
    public int MaxHealth => _maxHealth;
    public float Speed => _speed;
    public float Acceleration => _acceleration;
    public float Braking => _braking;
    public float LaunchDelay => _launchDelay;
    public float WeaveAmplitude => _weaveAmplitude;
    public float WeaveWavelength => _weaveWavelength;
    public float WeaveRampDistance => _weaveRampDistance;
  }
}
