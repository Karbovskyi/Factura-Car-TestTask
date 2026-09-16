namespace FacturaCar.Features.GameplayFeature
{
  public interface ICarConfig
  {
    Car Prefab { get; }
    float Speed { get; }
    float Acceleration { get; }
    float Braking { get; }
  }
}
