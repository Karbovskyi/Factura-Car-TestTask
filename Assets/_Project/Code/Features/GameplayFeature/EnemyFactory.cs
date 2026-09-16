using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemyFactory
  {
    private readonly IObjectResolver _resolver;
    private readonly IEnemyConfig _config;
    private readonly Transform _container;

    public EnemyFactory(IObjectResolver resolver, IEnemyConfig config)
    {
      _resolver = resolver;
      _config = config;
      _container = new GameObject("Enemies").transform;
    }

    public Enemy Create(Vector3 position) =>
      _resolver.Instantiate(_config.Prefab, position, Quaternion.LookRotation(Vector3.back), _container);
  }
}
