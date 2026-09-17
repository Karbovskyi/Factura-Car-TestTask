using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemyFactory
  {
    private readonly IObjectResolver _resolver;
    private readonly IEnemyConfig _config;
    private readonly ObjectPool<Enemy> _pool;
    private readonly Transform _container;

    public EnemyFactory(IObjectResolver resolver, IEnemyConfig config)
    {
      _resolver = resolver;
      _config = config;
      _container = new GameObject("Enemies").transform;
      _pool = new ObjectPool<Enemy>(CreateEnemy, OnGet, OnRelease);
    }

    public Enemy Create(Vector3 position)
    {
      Enemy enemy = _pool.Get();
      enemy.Spawn(position);

      return enemy;
    }

    private Enemy CreateEnemy()
    {
      Enemy enemy = _resolver.Instantiate(_config.Prefab, _container);
      enemy.Initialize(_pool);

      return enemy;
    }

    private static void OnGet(Enemy enemy) => enemy.gameObject.SetActive(true);

    private static void OnRelease(Enemy enemy) => enemy.gameObject.SetActive(false);
  }
}
