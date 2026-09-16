using System;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameplayMediator : IInitializable, IDisposable
  {
    private readonly Car _car;
    private readonly HealthBarView _healthBar;
    private readonly ResultView _resultView;

    public GameplayMediator(Car car, HealthBarView healthBar, ResultView resultView)
    {
      _car = car;
      _healthBar = healthBar;
      _resultView = resultView;
    }

    public void Initialize() => _car.HealthChanged += OnHealthChanged;

    public void Dispose() => _car.HealthChanged -= OnHealthChanged;

    public void ShowWin() => _resultView.ShowWin();

    public void ShowLose() => _resultView.ShowLose();

    public void HideResult() => _resultView.Hide();

    private void OnHealthChanged() => _healthBar.SetHealth(_car.HealthFraction);
  }
}
