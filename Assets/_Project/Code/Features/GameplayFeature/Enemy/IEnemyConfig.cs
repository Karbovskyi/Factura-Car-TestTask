namespace FacturaCar.Features.GameplayFeature
{
  public interface IEnemyConfig
  {
    Enemy Prefab { get; }
    EnemySplat SplatPrefab { get; }
    int MaxHealth { get; }
    int ContactDamage { get; }
    float RunSpeed { get; }
    float AggroDistance { get; }
  }
}
