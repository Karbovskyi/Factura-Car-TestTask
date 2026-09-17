using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public interface IBulletConfig
  {
    Bullet Prefab { get; }
    float Speed { get; }
    float Lifetime { get; }
    int Damage { get; }
    float HitRadius { get; }
    LayerMask HitLayers { get; }
  }
}
