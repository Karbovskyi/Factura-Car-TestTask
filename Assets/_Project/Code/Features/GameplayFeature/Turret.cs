using UnityEngine;
using VContainer;

namespace FacturaCar.Features.GameplayFeature
{
  public class Turret : MonoBehaviour
  {
    [SerializeField] private Transform _muzzle;

    private ITurretConfig _config;
    private BulletFactory _bulletFactory;
    private float _angle;
    private float _targetAngle;
    private bool _isFiring;
    private float _cooldown;

    [Inject]
    public void Construct(ITurretConfig config, BulletFactory bulletFactory)
    {
      _config = config;
      _bulletFactory = bulletFactory;
    }

    public void Aim(float screenWidths)
    {
      float angle = _targetAngle + screenWidths * _config.DegreesPerScreenWidth;
      _targetAngle = Mathf.Clamp(angle, -_config.MaxAngle, _config.MaxAngle);
    }

    public void ResetAim()
    {
      _angle = 0f;
      _targetAngle = 0f;
      ApplyAngle();
    }

    public void StartFiring() => _isFiring = true;

    public void StopFiring() => _isFiring = false;

    private void Update()
    {
      _angle = Mathf.MoveTowards(_angle, _targetAngle, _config.RotationSpeed * Time.deltaTime);
      ApplyAngle();

      _cooldown -= Time.deltaTime;

      if (_isFiring && _cooldown <= 0f)
        Fire();
    }

    private void ApplyAngle() => transform.localRotation = Quaternion.Euler(0f, _angle, 0f);

    private void Fire()
    {
      _bulletFactory.Create(_muzzle.position, _muzzle.rotation);
      _cooldown = 1f / _config.FireRate;
    }
  }
}
