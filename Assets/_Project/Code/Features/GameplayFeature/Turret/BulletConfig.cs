using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "BulletConfig", menuName = "Factura Car/Bullet Config")]
  public class BulletConfig : ScriptableObject, IBulletConfig
  {
    [SerializeField] private Bullet _prefab;
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(0f)] private float _lifetime;
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0f)] private float _hitRadius;
    [SerializeField] private LayerMask _hitLayers;

    public Bullet Prefab => _prefab;
    public float Speed => _speed;
    public float Lifetime => _lifetime;
    public int Damage => _damage;
    public float HitRadius => _hitRadius;
    public LayerMask HitLayers => _hitLayers;
  }
}
