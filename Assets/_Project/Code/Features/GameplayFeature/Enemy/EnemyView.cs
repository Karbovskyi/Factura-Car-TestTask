using PrimeTween;
using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemyView : MonoBehaviour
  {
    private static readonly int IdleState = Animator.StringToHash("Idle");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int RunSpeedMultiplier = Animator.StringToHash("RunSpeedMultiplier");
    private static readonly int Hit = Animator.StringToHash("Hit");

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField, Min(0.01f)] private float _runClipSpeed;

    [Header("Hit Flash")]
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Material _hitFlashMaterial;
    [SerializeField, Min(0f)] private float _hitFlashDuration;

    [Header("Hit Jolt")]
    [SerializeField] private Transform _body;
    [SerializeField, Min(0f)] private float _hitPushDistance;
    [SerializeField] private float _hitTiltAngle;
    [SerializeField, Min(0f)] private float _hitDuration;
    [SerializeField, Min(0f)] private float _hitFrequency;

    [Header("Health Bar")]
    [SerializeField] private HealthBarView _healthBar;

    private Material _material;
    private Transform _camera;
    private Sequence _hit;

    public void ResetToIdle()
    {
      _hit.Stop();
      ResetBody();
      ShowMaterial();
      _healthBar.SetHealthInstantly(1f);
      _healthBar.gameObject.SetActive(false);
      PlayIdle();
      _animator.Play(IdleState, 0, Random.value);
    }

    public void PlayIdle() => _animator.SetBool(IsRunning, false);

    public void PlayRun(float speed)
    {
      _animator.SetFloat(RunSpeedMultiplier, speed / _runClipSpeed);
      _animator.SetBool(IsRunning, true);
    }

    public void ShowHealth(float fraction)
    {
      _healthBar.gameObject.SetActive(true);
      _healthBar.SetHealth(fraction);
    }

    public void PlayHit(Vector3 hitDirection)
    {
      Vector3 push = Vector3.ProjectOnPlane(transform.InverseTransformDirection(hitDirection), Vector3.up).normalized;
      Vector3 tilt = Vector3.Cross(Vector3.up, push) * _hitTiltAngle;

      _hit.Stop();
      ResetBody();
      _renderer.sharedMaterial = _hitFlashMaterial;
      _animator.SetTrigger(Hit);

      _hit = Sequence.Create()
        .Group(Tween.PunchLocalPosition(_body, push * _hitPushDistance, _hitDuration, _hitFrequency))
        .Group(Tween.PunchLocalRotation(_body, tilt, _hitDuration, _hitFrequency))
        .InsertCallback(_hitFlashDuration, this, self => self.ShowMaterial());
    }

    private void Awake()
    {
      _material = _renderer.sharedMaterial;
      _camera = Camera.main.transform;
    }

    private void LateUpdate() => _healthBar.transform.rotation = _camera.rotation;

    private void ResetBody()
    {
      _body.localPosition = Vector3.zero;
      _body.localRotation = Quaternion.identity;
    }

    private void ShowMaterial() => _renderer.sharedMaterial = _material;
  }
}
