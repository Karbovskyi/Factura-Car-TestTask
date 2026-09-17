using System;
using UnityEngine;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Car : MonoBehaviour
  {
    private const float WeaveSlopeStep = 0.1f;

    [SerializeField] private Turret _turret;
    [SerializeField] private CarView _view;

    private ICarConfig _config;
    private Vector3 _startPosition;
    private int _health;
    private float _speed;
    private float _targetSpeed;
    private float _launchDelay;
    private float _distance;

    public event Action HealthChanged;
    public event Action<int, Vector3> Damaged;
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

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
      if (_health <= 0)
        return;

      _health = Mathf.Max(_health - damage, 0);
      _view.PlayHit(hitPoint);
      Damaged?.Invoke(damage, hitPoint);
      HealthChanged?.Invoke();

      if (_health > 0)
        return;

      Explode();
      Died?.Invoke();
    }

    public void ResetToStart()
    {
      _distance = 0f;
      PlaceOnRoad();
      _view.ResetExplosion();
      _view.ClearTracks();
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

    private void Explode()
    {
      Vector3 velocity = transform.forward * _speed;

      _speed = 0f;
      _targetSpeed = 0f;
      _view.PlayExplosion(velocity);
    }

    private void Drive()
    {
      float rate = _targetSpeed > _speed ? _config.Acceleration : _config.Braking;

      _speed = Mathf.MoveTowards(_speed, _targetSpeed, rate * Time.deltaTime);
      _distance += _speed * Time.deltaTime;
      PlaceOnRoad();
    }

    private void PlaceOnRoad()
    {
      float offset = WeaveOffset(_distance);
      float slope = (WeaveOffset(_distance + WeaveSlopeStep) - offset) / WeaveSlopeStep;

      transform.SetPositionAndRotation(
        _startPosition + Vector3.forward * _distance + Vector3.right * offset,
        Quaternion.LookRotation(new Vector3(slope, 0f, 1f)));
    }

    private float WeaveOffset(float distance)
    {
      float ramp = Mathf.SmoothStep(0f, 1f, distance / _config.WeaveRampDistance);
      float wave = Mathf.Sin(distance / _config.WeaveWavelength * 2f * Mathf.PI);

      return wave * _config.WeaveAmplitude * ramp;
    }
  }
}
