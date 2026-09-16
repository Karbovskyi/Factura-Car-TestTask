using UnityEngine;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Car : MonoBehaviour
  {
    [SerializeField] private Turret _turret;

    private ICarConfig _config;
    private Vector3 _startPosition;
    private float _speed;
    private float _targetSpeed;

    public Turret Turret => _turret;

    [Inject]
    public void Construct(ICarConfig config)
    {
      _config = config;
    }

    public void StartDriving() => _targetSpeed = _config.Speed;

    public void Stop() => _targetSpeed = 0f;

    public void ResetToStart()
    {
      transform.position = _startPosition;
      _speed = 0f;
      _targetSpeed = 0f;
    }

    private void Awake()
    {
      _startPosition = transform.position;
    }

    private void Update()
    {
      float rate = _targetSpeed > _speed ? _config.Acceleration : _config.Braking;

      _speed = Mathf.MoveTowards(_speed, _targetSpeed, rate * Time.deltaTime);
      transform.position += transform.forward * (_speed * Time.deltaTime);
    }
  }
}
