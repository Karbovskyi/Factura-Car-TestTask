using Cysharp.Threading.Tasks;

namespace FacturaCar.Capabilities.AppFlowCapability
{
  public interface IAppStateMachine
  {
    UniTask Enter<TState>() where TState : class, IState;
  }
}
