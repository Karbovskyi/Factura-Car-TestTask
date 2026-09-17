using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public interface ILevelConfig
  {
    float Length { get; }
    GameObject GroundSegmentPrefab { get; }
    int EnemyCount { get; }
    float EnemySpawnStartDistance { get; }
    float EnemySpawnHalfWidth { get; }
    float EnemyMinSpacing { get; }
    float EnemySpawnAheadDistance { get; }
    float EnemyDespawnBehindDistance { get; }
    DamageNumber DamageNumberPrefab { get; }
  }
}
