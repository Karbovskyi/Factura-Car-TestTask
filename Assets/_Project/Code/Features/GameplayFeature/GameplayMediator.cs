using System;
using UnityEngine;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameplayMediator : IInitializable, IDisposable
  {
    private readonly Car _car;
    private readonly HealthBarView _healthBar;
    private readonly ResultView _resultView;
    private readonly DamageNumberFactory _damageNumberFactory;

    public GameplayMediator(
      Car car,
      HealthBarView healthBar,
      ResultView resultView,
      DamageNumberFactory damageNumberFactory)
    {
      _car = car;
      _healthBar = healthBar;
      _resultView = resultView;
      _damageNumberFactory = damageNumberFactory;
    }

    public void Initialize()
    {
      _car.HealthChanged += OnHealthChanged;
      _car.Damaged += OnDamaged;
    }

    public void Dispose()
    {
      _car.HealthChanged -= OnHealthChanged;
      _car.Damaged -= OnDamaged;
    }

    public void ShowWin() => _resultView.ShowWin();

    public void ShowLose() => _resultView.ShowLose();

    public void HideResult() => _resultView.Hide();

    private void OnHealthChanged() => _healthBar.SetHealth(_car.HealthFraction);

    private void OnDamaged(int damage, Vector3 hitPoint) =>
      _damageNumberFactory.Create(damage, _car.transform, hitPoint);
  }
}
