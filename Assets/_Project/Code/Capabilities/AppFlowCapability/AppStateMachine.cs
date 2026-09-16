using VContainer;

namespace FacturaCar.Capabilities.AppFlowCapability
{
  class AppStateMachine : IAppStateMachine
  {
    private readonly IObjectResolver _resolver;

    private IState _activeState;

    public AppStateMachine(IObjectResolver resolver)
    {
      _resolver = resolver;
    }

    public void Enter<TState>() where TState : class, IState
    {
      _activeState?.Exit();

      _activeState = _resolver.Resolve<TState>();
      _activeState.Enter();
    }
  }
}
