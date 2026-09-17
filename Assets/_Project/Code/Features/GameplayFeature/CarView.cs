using System;
using PrimeTween;
using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class CarView : MonoBehaviour
  {
    [Header("Chassis")]
    [SerializeField] private Transform _chassis;
    [SerializeField, Min(0f)] private float _wheelbase;

    [Header("Wheels")]
    [SerializeField] private Transform[] _wheels;
    [SerializeField, Min(0.01f)] private float _wheelRadius;
    [SerializeField, Min(0f)] private float _wheelspinSpeed;

    [Header("Launch Sway")]
    [SerializeField] private float _launchPitch;
    [SerializeField] private float _fishtailAngle;
    [SerializeField, Min(0f)] private float _fishtailFrequency;
    [SerializeField] private float _rumbleAngle;
    [SerializeField, Min(0f)] private float _rumbleFrequency;

    [Header("Dust")]
    [SerializeField] private ParticleSystem[] _wheelDust;
    [SerializeField, Min(0f)] private float _burnoutDustRate;
    [SerializeField, Min(0f)] private float _dustPerMeter;

    [Header("Hit Flash")]
    [SerializeField] private MeshRenderer[] _hitFlashRenderers;
    [SerializeField] private Material _hitFlashMaterial;
    [SerializeField, Min(0f)] private float _hitFlashDuration;

    [Header("Hit Jolt")]
    [SerializeField] private BoxCollider _hitShape;
    [SerializeField, Min(0f)] private float _hitPushDistance;
    [SerializeField] private float _hitTiltAngle;
    [SerializeField] private float _hitSpinAngle;
    [SerializeField, Min(0f)] private float _hitDuration;
    [SerializeField, Min(0f)] private float _hitFrequency;

    private Vector3 _chassisOrigin;
    private float _speed;
    private float _slip;
    private float _slipTime;
    private Material[][] _materials;
    private Material[][] _hitFlashMaterials;
    private Vector3 _hitOffset;
    private Vector3 _hitRotation;
    private Sequence _hit;

    public void SetMotion(float speed, float targetSpeed)
    {
      _speed = speed;
      _slip = targetSpeed > 0f ? Mathf.SmoothStep(0f, 1f, 1f - speed / targetSpeed) : 0f;
    }

    public void PlayHit(Vector3 hitPoint)
    {
      Vector3 point = transform.InverseTransformPoint(hitPoint);
      Vector3 push = PushDirection(point);
      Vector3 tilt = Vector3.Cross(Vector3.up, push) * _hitTiltAngle;
      float spin = Vector3.Cross(point - _hitShape.center, push).y * _hitSpinAngle;

      _hit.Stop();
      SetMaterials(_hitFlashMaterials);

      _hit = Sequence.Create()
        .Group(Tween.PunchCustom(this, Vector3.zero, HitShake(push * _hitPushDistance), (self, offset) => self._hitOffset = offset))
        .Group(Tween.PunchCustom(this, Vector3.zero, HitShake(new Vector3(tilt.x, spin, tilt.z)), (self, angles) => self._hitRotation = angles))
        .InsertCallback(_hitFlashDuration, this, self => self.SetMaterials(self._materials));
    }

    private void Awake()
    {
      _chassisOrigin = _chassis.localPosition;
      CacheMaterials();
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
      float pitch = _launchPitch * _slip - _hitRotation.x;
      float yaw = Mathf.Sin(_slipTime * _fishtailFrequency * 2f * Mathf.PI) * _fishtailAngle * _slip;
      float rumble = (Mathf.PerlinNoise1D(_slipTime * _rumbleFrequency) - 0.5f) * 2f * _rumbleAngle * _slip;
      float rearAxleLift = _wheelbase * Mathf.Sin(pitch * Mathf.Deg2Rad);

      _chassis.localRotation = Quaternion.Euler(rumble - pitch, yaw + _hitRotation.y, _hitRotation.z);
      _chassis.localPosition = _chassisOrigin + Vector3.up * rearAxleLift + _hitOffset;
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

    private Vector3 PushDirection(Vector3 point)
    {
      Vector3 fromCenter = point - _hitShape.center;
      float side = fromCenter.x / _hitShape.size.x;
      float front = fromCenter.z / _hitShape.size.z;

      return Mathf.Abs(side) > Mathf.Abs(front)
        ? new Vector3(-Mathf.Sign(side), 0f, 0f)
        : new Vector3(0f, 0f, -Mathf.Sign(front));
    }

    private ShakeSettings HitShake(Vector3 strength) => new ShakeSettings(strength, _hitDuration, _hitFrequency);

    private void CacheMaterials()
    {
      _materials = new Material[_hitFlashRenderers.Length][];
      _hitFlashMaterials = new Material[_hitFlashRenderers.Length][];

      for (int i = 0; i < _hitFlashRenderers.Length; i++)
      {
        _materials[i] = _hitFlashRenderers[i].sharedMaterials;
        _hitFlashMaterials[i] = new Material[_materials[i].Length];
        Array.Fill(_hitFlashMaterials[i], _hitFlashMaterial);
      }
    }

    private void SetMaterials(Material[][] materials)
    {
      for (int i = 0; i < _hitFlashRenderers.Length; i++)
        _hitFlashRenderers[i].sharedMaterials = materials[i];
    }
  }
}
