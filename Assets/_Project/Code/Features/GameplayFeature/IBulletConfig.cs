namespace FacturaCar.Features.GameplayFeature
{
  public interface IBulletConfig
  {
    Bullet Prefab { get; }
    float Speed { get; }
    float Lifetime { get; }
  }
}
