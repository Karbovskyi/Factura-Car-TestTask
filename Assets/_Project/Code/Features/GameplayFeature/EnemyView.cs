using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class EnemyView : MonoBehaviour
  {
    private static readonly int IdleState = Animator.StringToHash("Idle");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int RunSpeedMultiplier = Animator.StringToHash("RunSpeedMultiplier");

    [SerializeField] private Animator _animator;
    [SerializeField, Min(0.01f)] private float _runClipSpeed;

    public void PlayIdle() => _animator.SetBool(IsRunning, false);

    public void PlayRun(float speed)
    {
      _animator.SetFloat(RunSpeedMultiplier, speed / _runClipSpeed);
      _animator.SetBool(IsRunning, true);
    }

    private void Start() => _animator.Play(IdleState, 0, Random.value);
  }
}
