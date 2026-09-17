using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace FacturaCar.Features.GameplayFeature
{
  public class DamageNumber : MonoBehaviour
  {
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Vector3 _spawnOffset;
    [SerializeField, Min(0f)] private float _spawnSpread;
    [SerializeField] private TweenSettings<float> _pop;
    [SerializeField] private TweenSettings<float> _rise;
    [SerializeField] private TweenSettings<float> _fade;

    private IObjectPool<DamageNumber> _pool;
    private Transform _camera;
    private Vector3 _startPosition;
    private Sequence _animation;

    public void Initialize(IObjectPool<DamageNumber> pool) => _pool = pool;

    public void Show(int damage, Vector3 localPoint)
    {
      _text.SetText("{0}", damage);
      _startPosition = localPoint + _spawnOffset + Vector3.right * Random.Range(-_spawnSpread, _spawnSpread);
      ShowRise(_rise.startValue);
      ShowFade(_fade.startValue);
      transform.localScale = Vector3.one * _pop.startValue;

      _animation.Stop();
      _animation = Sequence.Create()
        .Group(Tween.Scale(transform, _pop))
        .Group(Tween.Custom(this, _rise, (self, height) => self.ShowRise(height)))
        .Group(Tween.Custom(this, _fade, (self, alpha) => self.ShowFade(alpha)))
        .OnComplete(this, self => self._pool.Release(self));
    }

    private void Awake()
    {
      _camera = Camera.main.transform;
    }

    private void LateUpdate() => transform.rotation = _camera.rotation;

    private void ShowRise(float height) => transform.localPosition = _startPosition + Vector3.up * height;

    private void ShowFade(float alpha) => _text.alpha = alpha;
  }
}
