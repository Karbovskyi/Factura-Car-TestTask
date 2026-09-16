namespace FacturaCar.Features.GameplayFeature
{
  public interface ICarConfig
  {
    Car Prefab { get; }
    int MaxHealth { get; }
    float Speed { get; }
    float Acceleration { get; }
    float Braking { get; }
    float LaunchDelay { get; }
  }
}
