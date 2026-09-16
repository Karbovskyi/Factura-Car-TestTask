using System.Collections.Generic;
using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemySpawner
  {
    private readonly EnemyFactory _enemyFactory;
    private readonly ILevelConfig _levelConfig;
    private readonly List<Enemy> _enemies = new List<Enemy>();

    public EnemySpawner(EnemyFactory enemyFactory, ILevelConfig levelConfig)
    {
      _enemyFactory = enemyFactory;
      _levelConfig = levelConfig;
    }

    public void Respawn()
    {
      Clear();

      for (int i = 0; i < _levelConfig.EnemyCount; i++)
        _enemies.Add(_enemyFactory.Create(RandomPosition()));
    }

    public void StopAll()
    {
      foreach (Enemy enemy in _enemies)
        enemy.Stop();
    }

    private void Clear()
    {
      foreach (Enemy enemy in _enemies)
        Object.Destroy(enemy.gameObject);

      _enemies.Clear();
    }

    private Vector3 RandomPosition()
    {
      float x = Random.Range(-_levelConfig.EnemySpawnHalfWidth, _levelConfig.EnemySpawnHalfWidth);
      float z = Random.Range(_levelConfig.EnemySpawnStartDistance, _levelConfig.Length);

      return new Vector3(x, 0f, z);
    }
  }
}
