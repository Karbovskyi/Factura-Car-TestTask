using System.Threading;
using Cysharp.Threading.Tasks;
using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelReadyState : IState
  {
    private readonly GroundFactory _groundFactory;
    private readonly CarFactory _carFactory;

    public LevelReadyState(GroundFactory groundFactory, CarFactory carFactory)
    {
      _groundFactory = groundFactory;
      _carFactory = carFactory;
    }

    public UniTask EnterAsync(CancellationToken ct)
    {
      _groundFactory.Create();
      _carFactory.Create();

      return UniTask.CompletedTask;
    }

    public UniTask ExitAsync() => UniTask.CompletedTask;
  }
}
