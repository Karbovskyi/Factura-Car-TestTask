using UnityEngine;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Enemy : MonoBehaviour
  {
    [SerializeField] private EnemyView _view;

    private IEnemyConfig _config;
    private Car _car;
    private EnemySplatFactory _splatFactory;
    private DamageNumberFactory _damageNumberFactory;
    private int _health;
    private bool _isChasing;
    private bool _isStopped;

    [Inject]
    public void Construct(
      IEnemyConfig config,
      Car car,
      EnemySplatFactory splatFactory,
      DamageNumberFactory damageNumberFactory)
    {
      _config = config;
      _car = car;
      _splatFactory = splatFactory;
      _damageNumberFactory = damageNumberFactory;
      _health = config.MaxHealth;
    }

    public void TakeDamage(int damage, Vector3 hitDirection)
    {
      _health -= damage;
      _damageNumberFactory.Create(damage, transform.position);

      if (_health <= 0)
      {
        Die();
        return;
      }

      _view.ShowHealth((float)_health / _config.MaxHealth);
      _view.PlayHit(hitDirection);
    }

    public void Stop()
    {
      if (!isActiveAndEnabled)
        return;

      _isStopped = true;
      _view.PlayIdle();
    }

    private void Update()
    {
      if (_isStopped)
        return;

      Vector3 toCar = _car.transform.position - transform.position;
      toCar.y = 0f;

      if (!_isChasing && toCar.magnitude <= _config.AggroDistance)
        StartChasing();

      if (_isChasing && toCar != Vector3.zero)
        RunTowards(toCar);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!other.TryGetComponent(out Car car))
        return;

      car.TakeDamage(_config.ContactDamage, transform.position);
      Die();
    }

    private void StartChasing()
    {
      _isChasing = true;
      _view.PlayRun(_config.RunSpeed);
    }

    private void RunTowards(Vector3 toCar)
    {
      transform.rotation = Quaternion.LookRotation(toCar);
      transform.position = Vector3.MoveTowards(
        transform.position,
        transform.position + toCar,
        _config.RunSpeed * Time.deltaTime);
    }

    private void Die()
    {
      _splatFactory.Create(transform.position);
      gameObject.SetActive(false);
    }
  }
}
