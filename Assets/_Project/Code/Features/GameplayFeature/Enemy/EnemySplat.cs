using UnityEngine;
using UnityEngine.Pool;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemySplat : MonoBehaviour
  {
    [SerializeField] private ParticleSystem _particles;

    private IObjectPool<EnemySplat> _pool;

    public void Initialize(IObjectPool<EnemySplat> pool) => _pool = pool;

    public void Play(Vector3 position)
    {
      transform.position = position;
      _particles.Play();
    }

    private void OnParticleSystemStopped() => _pool.Release(this);
  }
}
