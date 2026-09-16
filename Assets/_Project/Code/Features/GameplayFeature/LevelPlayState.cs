using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelPlayState : IState
  {
    private readonly IAppStateMachine _appStateMachine;
    private readonly Car _car;
    private readonly Turret _turret;
    private readonly FinishLine _finishLine;
    private readonly GameInput _gameInput;

    public LevelPlayState(
      IAppStateMachine appStateMachine,
      Car car,
      Turret turret,
      FinishLine finishLine,
      GameInput gameInput)
    {
      _appStateMachine = appStateMachine;
      _car = car;
      _turret = turret;
      _finishLine = finishLine;
      _gameInput = gameInput;
    }

    public void Enter()
    {
      _car.StartDriving();
      _finishLine.Reached += OnFinishReached;
      _gameInput.Dragged += OnDragged;
    }

    public void Exit()
    {
      _finishLine.Reached -= OnFinishReached;
      _gameInput.Dragged -= OnDragged;
    }

    private void OnFinishReached() => _appStateMachine.Enter<LevelWinState>();

    private void OnDragged(float screenWidths) => _turret.Aim(screenWidths);
  }
}
