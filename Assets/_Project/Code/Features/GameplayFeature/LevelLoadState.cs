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
    private readonly CameraFollow _cameraFollow;

    public LevelLoadState(
      IAppStateMachine appStateMachine,
      GroundFactory groundFactory,
      FinishLine finishLine,
      ILevelConfig levelConfig,
      Car car,
      CameraFollow cameraFollow)
    {
      _appStateMachine = appStateMachine;
      _groundFactory = groundFactory;
      _finishLine = finishLine;
      _levelConfig = levelConfig;
      _car = car;
      _cameraFollow = cameraFollow;
    }

    public void Enter()
    {
      _groundFactory.Create();
      _finishLine.PlaceAt(_levelConfig.Length);
      _cameraFollow.Follow(_car.transform);

      _appStateMachine.Enter<LevelReadyState>();
    }

    public void Exit()
    {
    }
  }
}
