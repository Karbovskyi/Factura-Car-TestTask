using System.Threading;
using Cysharp.Threading.Tasks;

namespace FacturaCar.Capabilities.AppFlowCapability
{
  public interface IState : IExitableState
  {
    UniTask EnterAsync(CancellationToken ct);
  }
}
