using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "CarConfig", menuName = "Factura Car/Car Config")]
  public class CarConfig : ScriptableObject, ICarConfig
  {
    [SerializeField] private Car _prefab;
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(0f)] private float _acceleration;
    [SerializeField, Min(0f)] private float _braking;

    public Car Prefab => _prefab;
    public float Speed => _speed;
    public float Acceleration => _acceleration;
    public float Braking => _braking;
  }
}
