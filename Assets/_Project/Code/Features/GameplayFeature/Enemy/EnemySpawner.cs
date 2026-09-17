using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemySpawner : ITickable
  {
    private readonly EnemyFactory _enemyFactory;
    private readonly ILevelConfig _levelConfig;
    private readonly Car _car;
    private readonly Queue<Vector3> _pendingPositions = new Queue<Vector3>();
    private readonly List<Enemy> _enemies = new List<Enemy>();
    private bool _isStopped = true;

    public EnemySpawner(EnemyFactory enemyFactory, ILevelConfig levelConfig, Car car)
    {
      _enemyFactory = enemyFactory;
      _levelConfig = levelConfig;
      _car = car;
    }

    public void Respawn()
    {
      Clear();
      PlanPositions();
      _isStopped = false;
      SpawnAhead();
    }

    public void StopAll()
    {
      _isStopped = true;

      foreach (Enemy enemy in _enemies)
        enemy.Stop();
    }

    public void Tick()
    {
      if (_isStopped)
        return;

      SpawnAhead();
      DespawnPassed();
    }

    private void Clear()
    {
      for (int i = _enemies.Count - 1; i >= 0; i--)
        _enemies[i].Despawn();

      _pendingPositions.Clear();
    }

    private void PlanPositions()
    {
      float slotLength = (_levelConfig.Length - _levelConfig.EnemySpawnStartDistance) / _levelConfig.EnemyCount;
      float jitter = Mathf.Max(slotLength - _levelConfig.EnemyMinSpacing, 0f);

      for (int i = 0; i < _levelConfig.EnemyCount; i++)
      {
        float x = Random.Range(-_levelConfig.EnemySpawnHalfWidth, _levelConfig.EnemySpawnHalfWidth);
        float z = _levelConfig.EnemySpawnStartDistance + i * slotLength + Random.Range(0f, jitter);

        _pendingPositions.Enqueue(new Vector3(x, 0f, z));
      }
    }

    private void SpawnAhead()
    {
      float spawnLimit = _car.transform.position.z + _levelConfig.EnemySpawnAheadDistance;

      while (_pendingPositions.Count > 0 && _pendingPositions.Peek().z <= spawnLimit)
        Spawn(_pendingPositions.Dequeue());
    }

    private void DespawnPassed()
    {
      float despawnLimit = _car.transform.position.z - _levelConfig.EnemyDespawnBehindDistance;

      for (int i = _enemies.Count - 1; i >= 0; i--)
      {
        if (_enemies[i].transform.position.z < despawnLimit)
          _enemies[i].Despawn();
      }
    }

    private void Spawn(Vector3 position)
    {
      Enemy enemy = _enemyFactory.Create(position);

      enemy.Despawned += OnEnemyDespawned;
      _enemies.Add(enemy);
    }

    private void OnEnemyDespawned(Enemy enemy)
    {
      enemy.Despawned -= OnEnemyDespawned;
      _enemies.Remove(enemy);
    }
  }
}
