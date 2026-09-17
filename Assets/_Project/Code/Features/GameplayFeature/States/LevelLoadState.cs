using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelLoadState : IState
  {
    private readonly IAppStateMachine _appStateMachine;
    private readonly GroundFactory _groundFactory;
    private readonly FinishLine _finishLine;
    private readonly ILevelConfig _levelConfig;
    private readonly Car _car;
    private readonly CarCamera _carCamera;

    public LevelLoadState(
      IAppStateMachine appStateMachine,
      GroundFactory groundFactory,
      FinishLine finishLine,
      ILevelConfig levelConfig,
      Car car,
      CarCamera carCamera)
    {
      _appStateMachine = appStateMachine;
      _groundFactory = groundFactory;
      _finishLine = finishLine;
      _levelConfig = levelConfig;
      _car = car;
      _carCamera = carCamera;
    }

    public void Enter()
    {
      _groundFactory.Create();
      _finishLine.PlaceAt(_levelConfig.Length);
      _carCamera.Follow(_car.transform);

      _appStateMachine.Enter<LevelReadyState>();
    }

    public void Exit()
    {
    }
  }
}
