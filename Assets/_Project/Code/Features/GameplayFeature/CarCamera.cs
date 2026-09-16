using PrimeTween;
using Unity.Cinemachine;
using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class CarCamera : MonoBehaviour
  {
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private CinemachineFollow _follow;
    [SerializeField] private CinemachineRotationComposer _composer;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _noise;
    [SerializeField] private AnimationCurve _azimuth;
    [SerializeField] private AnimationCurve _elevation;
    [SerializeField] private AnimationCurve _distance;
    [SerializeField] private AnimationCurve _lookHeight;
    [SerializeField] private AnimationCurve _lookAhead;
    [SerializeField] private AnimationCurve _shake;

    private Tween _launch;

    public void Follow(Transform target) => _camera.Follow = target;

    public void ShowPreview()
    {
      _launch.Stop();
      ShowAt(0f);
    }

    public void PlayLaunch()
    {
      float duration = LaunchDuration();

      _launch.Stop();
      _launch = Tween.Custom(this, 0f, duration, duration, (self, time) => self.ShowAt(time), Ease.Linear);
    }

    private void ShowAt(float time)
    {
      Quaternion direction = Quaternion.Euler(-_elevation.Evaluate(time), _azimuth.Evaluate(time), 0f);

      _follow.FollowOffset = direction * Vector3.forward * _distance.Evaluate(time);
      _composer.TargetOffset = new Vector3(0f, _lookHeight.Evaluate(time), _lookAhead.Evaluate(time));
      _noise.AmplitudeGain = _shake.Evaluate(time);
    }

    private float LaunchDuration()
    {
      AnimationCurve[] curves = { _azimuth, _elevation, _distance, _lookHeight, _lookAhead, _shake };
      float duration = 0f;

      foreach (AnimationCurve curve in curves)
        duration = Mathf.Max(duration, curve.keys[curve.length - 1].time);

      return duration;
    }
  }
}
