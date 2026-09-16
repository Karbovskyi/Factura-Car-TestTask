using System.Threading;
using Cysharp.Threading.Tasks;
using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelReadyState : IState
  {
    public UniTask EnterAsync(CancellationToken ct) => UniTask.CompletedTask;

    public UniTask ExitAsync() => UniTask.CompletedTask;
  }
}
