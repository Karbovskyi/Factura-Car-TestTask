using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Factura Car/Enemy Config")]
  public class EnemyConfig : ScriptableObject, IEnemyConfig
  {
    [SerializeField] private Enemy _prefab;
    [SerializeField] private EnemySplat _splatPrefab;
    [SerializeField, Min(1)] private int _maxHealth;
    [SerializeField, Min(0)] private int _contactDamage;
    [SerializeField, Min(0f)] private float _runSpeed;
    [SerializeField, Min(0f)] private float _aggroDistance;

    public Enemy Prefab => _prefab;
    public EnemySplat SplatPrefab => _splatPrefab;
    public int MaxHealth => _maxHealth;
    public int ContactDamage => _contactDamage;
    public float RunSpeed => _runSpeed;
    public float AggroDistance => _aggroDistance;
  }
}
