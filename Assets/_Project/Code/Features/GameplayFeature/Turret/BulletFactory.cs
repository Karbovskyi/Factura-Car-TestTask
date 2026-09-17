using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class BulletFactory
  {
    private readonly IObjectResolver _resolver;
    private readonly IBulletConfig _config;
    private readonly ObjectPool<Bullet> _pool;
    private readonly Transform _container;

    public BulletFactory(IObjectResolver resolver, IBulletConfig config)
    {
      _resolver = resolver;
      _config = config;
      _container = new GameObject("Bullets").transform;
      _pool = new ObjectPool<Bullet>(CreateBullet, OnGet, OnRelease);
    }

    public void Create(Vector3 position, Quaternion rotation) => _pool.Get().Launch(position, rotation);

    private Bullet CreateBullet()
    {
      Bullet bullet = _resolver.Instantiate(_config.Prefab, _container);
      bullet.Initialize(_pool);

      return bullet;
    }

    private static void OnGet(Bullet bullet) => bullet.gameObject.SetActive(true);

    private static void OnRelease(Bullet bullet) => bullet.gameObject.SetActive(false);
  }
}
