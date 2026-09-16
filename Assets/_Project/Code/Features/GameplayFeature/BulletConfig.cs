using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "BulletConfig", menuName = "Factura Car/Bullet Config")]
  public class BulletConfig : ScriptableObject, IBulletConfig
  {
    [SerializeField] private Bullet _prefab;
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(0f)] private float _lifetime;

    public Bullet Prefab => _prefab;
    public float Speed => _speed;
    public float Lifetime => _lifetime;
  }
}
