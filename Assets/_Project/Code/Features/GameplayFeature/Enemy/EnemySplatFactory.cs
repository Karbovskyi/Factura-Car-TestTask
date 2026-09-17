using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemySplatFactory
  {
    private readonly IObjectResolver _resolver;
    private readonly IEnemyConfig _config;
    private readonly ObjectPool<EnemySplat> _pool;
    private readonly Transform _container;

    public EnemySplatFactory(IObjectResolver resolver, IEnemyConfig config)
    {
      _resolver = resolver;
      _config = config;
      _container = new GameObject("EnemySplats").transform;
      _pool = new ObjectPool<EnemySplat>(CreateSplat, OnGet, OnRelease);
    }

    public void Create(Vector3 position) => _pool.Get().Play(position);

    private EnemySplat CreateSplat()
    {
      EnemySplat splat = _resolver.Instantiate(_config.SplatPrefab, _container);
      splat.Initialize(_pool);

      return splat;
    }

    private static void OnGet(EnemySplat splat) => splat.gameObject.SetActive(true);

    private static void OnRelease(EnemySplat splat) => splat.gameObject.SetActive(false);
  }
}
