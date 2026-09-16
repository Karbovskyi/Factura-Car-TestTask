using FacturaCar.Capabilities.AppFlowCapability;
using FacturaCar.Features.GameplayFeature;
using VContainer.Unity;

namespace FacturaCar.Bootstrap
{
  public class GameEntryPoint : IStartable
  {
    private readonly IAppStateMachine _appStateMachine;

    public GameEntryPoint(IAppStateMachine appStateMachine)
    {
      _appStateMachine = appStateMachine;
    }

    public void Start() => _appStateMachine.Enter<LevelLoadState>();
  }
}
