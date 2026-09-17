using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class DamageNumberFactory
  {
    private readonly IObjectResolver _resolver;
    private readonly ILevelConfig _config;
    private readonly ObjectPool<DamageNumber> _pool;
    private readonly Transform _container;

    public DamageNumberFactory(IObjectResolver resolver, ILevelConfig config)
    {
      _resolver = resolver;
      _config = config;
      _container = new GameObject("DamageNumbers").transform;
      _pool = new ObjectPool<DamageNumber>(CreateNumber, OnGet, OnRelease);
    }

    public void Create(int damage, Vector3 point) => Create(damage, _container, point);

    public void Create(int damage, Transform anchor, Vector3 point)
    {
      DamageNumber number = _pool.Get();

      number.transform.SetParent(anchor, false);
      number.Show(damage, anchor.InverseTransformPoint(point));
    }

    private DamageNumber CreateNumber()
    {
      DamageNumber number = _resolver.Instantiate(_config.DamageNumberPrefab, _container);
      number.Initialize(_pool);

      return number;
    }

    private static void OnGet(DamageNumber number) => number.gameObject.SetActive(true);

    private void OnRelease(DamageNumber number)
    {
      number.gameObject.SetActive(false);
      number.transform.SetParent(_container, false);
    }
  }
}
