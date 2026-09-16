using UnityEngine;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Enemy : MonoBehaviour
  {
    private IEnemyConfig _config;
    private Car _car;
    private int _health;
    private bool _isChasing;
    private bool _isStopped;

    [Inject]
    public void Construct(IEnemyConfig config, Car car)
    {
      _config = config;
      _car = car;
      _health = config.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
      _health -= damage;

      if (_health <= 0)
        Die();
    }

    public void Stop() => _isStopped = true;

    private void Update()
    {
      if (_isStopped)
        return;

      Vector3 toCar = _car.transform.position - transform.position;
      toCar.y = 0f;

      if (!_isChasing)
        _isChasing = toCar.magnitude <= _config.AggroDistance;

      if (_isChasing && toCar != Vector3.zero)
        RunTowards(toCar);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!other.TryGetComponent(out Car car))
        return;

      car.TakeDamage(_config.ContactDamage);
      Die();
    }

    private void RunTowards(Vector3 toCar)
    {
      transform.rotation = Quaternion.LookRotation(toCar);
      transform.position = Vector3.MoveTowards(
        transform.position,
        transform.position + toCar,
        _config.RunSpeed * Time.deltaTime);
    }

    private void Die() => gameObject.SetActive(false);
  }
}
