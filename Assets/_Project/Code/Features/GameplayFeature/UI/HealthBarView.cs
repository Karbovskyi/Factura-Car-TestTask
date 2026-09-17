using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace FacturaCar.Features.GameplayFeature
{
  public class HealthBarView : MonoBehaviour
  {
    [SerializeField] private RectTransform _fill;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Gradient _colorByHealth;
    [SerializeField] private TweenSettings _fillTween;

    private float _shownHealth = 1f;

    public void SetHealth(float fraction)
    {
      TweenSettings<float> settings = new TweenSettings<float>(_shownHealth, fraction, _fillTween);

      Tween.StopAll(onTarget: this);
      Tween.Custom(this, settings, (self, value) => self.ShowHealth(value));
    }

    public void SetHealthInstantly(float fraction)
    {
      Tween.StopAll(onTarget: this);
      ShowHealth(fraction);
    }

    private void ShowHealth(float fraction)
    {
      _shownHealth = fraction;
      _fill.anchorMax = new Vector2(fraction, _fill.anchorMax.y);
      _fillImage.color = _colorByHealth.Evaluate(fraction);
    }
  }
}
