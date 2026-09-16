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
      float step = _config.Speed * Time.deltaTime;

      if (TryHitEnemy(step))
      {
        _pool.Release(this);
        return;
      }

      transform.position += transform.forward * step;
      _flightTime += Time.deltaTime;

      if (_flightTime >= _config.Lifetime)
        _pool.Release(this);
    }

    private bool TryHitEnemy(float step)
    {
      bool isHit = Physics.SphereCast(
        transform.position,
        _config.HitRadius,
        transform.forward,
        out RaycastHit hit,
        step,
        _config.HitLayers,
        QueryTriggerInteraction.Collide);

      if (!isHit || !hit.collider.TryGetComponent(out Enemy enemy))
        return false;

      enemy.TakeDamage(_config.Damage);

      return true;
    }
  }
}
