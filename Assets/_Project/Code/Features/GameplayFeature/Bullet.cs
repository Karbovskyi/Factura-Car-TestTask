using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Bullet : MonoBehaviour
  {
    [SerializeField] private TrailRenderer _trail;

    private IBulletConfig _config;
    private IObjectPool<Bullet> _pool;
    private float _flightTime;

    [Inject]
    public void Construct(IBulletConfig config)
    {
      _config = config;
    }

    public void Initialize(IObjectPool<Bullet> pool) => _pool = pool;

    public void Launch(Vector3 position, Quaternion rotation)
    {
      transform.SetPositionAndRotation(position, rotation);
      _trail.Clear();
      _flightTime = 0f;
    }

    private void Update()
    {
      transform.position += transform.forward * (_config.Speed * Time.deltaTime);
      _flightTime += Time.deltaTime;

      if (_flightTime >= _config.Lifetime)
        _pool.Release(this);
    }
  }
}
