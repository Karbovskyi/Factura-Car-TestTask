namespace FacturaCar.Features.GameplayFeature
{
  public interface IEnemyConfig
  {
    Enemy Prefab { get; }
    int MaxHealth { get; }
    int ContactDamage { get; }
    float RunSpeed { get; }
    float AggroDistance { get; }
  }
}
