using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class CarView : MonoBehaviour
  {
    [SerializeField] private Transform _chassis;
    [SerializeField] private Transform[] _wheels;
    [SerializeField, Min(0.01f)] private float _wheelRadius;
    [SerializeField, Min(0f)] private float _wheelbase;
    [SerializeField, Min(0f)] private float _wheelspinSpeed;
    [SerializeField] private ParticleSystem[] _wheelDust;
    [SerializeField, Min(0f)] private float _burnoutDustRate;
    [SerializeField, Min(0f)] private float _dustPerMeter;
    [SerializeField] private float _fishtailAngle;
    [SerializeField, Min(0f)] private float _fishtailFrequency;
    [SerializeField] private float _launchPitch;
    [SerializeField] private float _rumbleAngle;
    [SerializeField, Min(0f)] private float _rumbleFrequency;

    private Vector3 _chassisOrigin;
    private float _speed;
    private float _slip;
    private float _slipTime;

    public void SetMotion(float speed, float targetSpeed)
    {
      _speed = speed;
      _slip = targetSpeed > 0f ? Mathf.SmoothStep(0f, 1f, 1f - speed / targetSpeed) : 0f;
    }

    private void Awake()
    {
      _chassisOrigin = _chassis.localPosition;
    }

    private void Update()
    {
      _slipTime = _slip > 0f ? _slipTime + Time.deltaTime : 0f;

      SpinWheels();
      SwayChassis();
      EmitDust();
    }

    private void SpinWheels()
    {
      float surfaceSpeed = _speed + _slip * _wheelspinSpeed;
      float degrees = surfaceSpeed / _wheelRadius * Mathf.Rad2Deg * Time.deltaTime;

      foreach (Transform wheel in _wheels)
        wheel.Rotate(degrees, 0f, 0f, Space.Self);
    }

    private void SwayChassis()
    {
      float pitch = _launchPitch * _slip;
      float yaw = Mathf.Sin(_slipTime * _fishtailFrequency * 2f * Mathf.PI) * _fishtailAngle * _slip;
      float rumble = (Mathf.PerlinNoise1D(_slipTime * _rumbleFrequency) - 0.5f) * 2f * _rumbleAngle * _slip;
      float rearAxleLift = _wheelbase * Mathf.Sin(pitch * Mathf.Deg2Rad);

      _chassis.localRotation = Quaternion.Euler(rumble - pitch, yaw, 0f);
      _chassis.localPosition = _chassisOrigin + Vector3.up * rearAxleLift;
    }

    private void EmitDust()
    {
      float rate = _slip * _burnoutDustRate + _speed * _dustPerMeter;

      foreach (ParticleSystem dust in _wheelDust)
      {
        ParticleSystem.EmissionModule emission = dust.emission;
        emission.rateOverTimeMultiplier = rate;
      }
    }
  }
}
