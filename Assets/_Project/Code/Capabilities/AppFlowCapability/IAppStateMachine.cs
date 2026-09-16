namespace FacturaCar.Capabilities.AppFlowCapability
{
  public interface IAppStateMachine
  {
    void Enter<TState>() where TState : class, IState;
  }
}
