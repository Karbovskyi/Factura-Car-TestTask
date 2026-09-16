using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Capabilities.AppFlowCapability
{
  class AppStateMachine : IAppStateMachine, IDisposable
  {
    private readonly LifetimeScope _parentScope;

    private IExitableState _activeState;
    private LifetimeScope _activeScope;
    private CancellationTokenSource _activeCts;

    public AppStateMachine(LifetimeScope parentScope)
    {
      _parentScope = parentScope;
    }

    public async UniTask Enter<TState>() where TState : class, IState
    {
      await ExitActiveAsync();

      CancellationTokenSource cts = new CancellationTokenSource();

      LifetimeScope scope = _parentScope.CreateChild(builder =>
      {
        builder.RegisterInstance(cts.Token);
        builder.Register<TState>(Lifetime.Scoped);
      }, typeof(TState).Name);

      TState state = scope.Container.Resolve<TState>();

      _activeState = state;
      _activeScope = scope;
      _activeCts = cts;

      await state.EnterAsync(cts.Token);
    }

    public void Dispose()
    {
      _activeCts?.Cancel();
      _activeCts?.Dispose();
    }

    private async UniTask ExitActiveAsync()
    {
      if (_activeState == null)
        return;

      IExitableState exitingState = _activeState;
      LifetimeScope exitingScope = _activeScope;
      CancellationTokenSource exitingCts = _activeCts;

      _activeState = null;
      _activeScope = null;
      _activeCts = null;

      exitingCts.Cancel();

      try
      {
        await exitingState.ExitAsync();
        await UniTask.Yield();
      }
      finally
      {
        exitingCts.Dispose();
        exitingScope.Dispose();
      }
    }
  }
}
