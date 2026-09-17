using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "LevelConfig", menuName = "Factura Car/Level Config")]
  public class LevelConfig : ScriptableObject, ILevelConfig
  {
    [SerializeField, Min(0f)] private float _length;
    [SerializeField] private GameObject _groundSegmentPrefab;
    [SerializeField, Min(0)] private int _enemyCount;
    [SerializeField, Min(0f)] private float _enemySpawnStartDistance;
    [SerializeField, Min(0f)] private float _enemySpawnHalfWidth;
    [SerializeField, Min(0f)] private float _enemyMinSpacing;
    [SerializeField, Min(0f)] private float _enemySpawnAheadDistance;
    [SerializeField, Min(0f)] private float _enemyDespawnBehindDistance;
    [SerializeField] private DamageNumber _damageNumberPrefab;

    public float Length => _length;
    public GameObject GroundSegmentPrefab => _groundSegmentPrefab;
    public int EnemyCount => _enemyCount;
    public float EnemySpawnStartDistance => _enemySpawnStartDistance;
    public float EnemySpawnHalfWidth => _enemySpawnHalfWidth;
    public float EnemyMinSpacing => _enemyMinSpacing;
    public float EnemySpawnAheadDistance => _enemySpawnAheadDistance;
    public float EnemyDespawnBehindDistance => _enemyDespawnBehindDistance;
    public DamageNumber DamageNumberPrefab => _damageNumberPrefab;
  }
}
