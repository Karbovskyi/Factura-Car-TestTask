using System;
using UnityEngine;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Car : MonoBehaviour
  {
    [SerializeField] private Turret _turret;
    [SerializeField] private CarView _view;

    private ICarConfig _config;
    private Vector3 _startPosition;
    private int _health;
    private float _speed;
    private float _targetSpeed;
    private float _launchDelay;

    public event Action HealthChanged;
    public event Action Died;

    public Turret Turret => _turret;
    public float HealthFraction => (float)_health / _config.MaxHealth;

    [Inject]
    public void Construct(ICarConfig config)
    {
      _config = config;
    }

    public void StartDriving()
    {
      _targetSpeed = _config.Speed;
      _launchDelay = _config.LaunchDelay;
    }

    public void Stop() => _targetSpeed = 0f;

    public void TakeDamage(int damage)
    {
      if (_health <= 0)
        return;

      _health = Mathf.Max(_health - damage, 0);
      HealthChanged?.Invoke();

      if (_health == 0)
        Died?.Invoke();
    }

    public void ResetToStart()
    {
      transform.position = _startPosition;
      _speed = 0f;
      _targetSpeed = 0f;
      _launchDelay = 0f;
      _health = _config.MaxHealth;
      HealthChanged?.Invoke();
    }

    private void Awake()
    {
      _startPosition = transform.position;
    }

    private void Update()
    {
      if (_launchDelay > 0f)
        _launchDelay -= Time.deltaTime;
      else
        Drive();

      _view.SetMotion(_speed, _targetSpeed);
    }

    private void Drive()
    {
      float rate = _targetSpeed > _speed ? _config.Acceleration : _config.Braking;

      _speed = Mathf.MoveTowards(_speed, _targetSpeed, rate * Time.deltaTime);
      transform.position += transform.forward * (_speed * Time.deltaTime);
    }
  }
}
