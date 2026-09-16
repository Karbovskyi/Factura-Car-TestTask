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
    private readonly CarCamera _carCamera;

    public LevelPlayState(
      IAppStateMachine appStateMachine,
      Car car,
      Turret turret,
      FinishLine finishLine,
      GameInput gameInput,
      CarCamera carCamera)
    {
      _appStateMachine = appStateMachine;
      _car = car;
      _turret = turret;
      _finishLine = finishLine;
      _gameInput = gameInput;
      _carCamera = carCamera;
    }

    public void Enter()
    {
      _car.StartDriving();
      _carCamera.PlayLaunch();

      if (_gameInput.IsPressed)
        _turret.StartFiring();

      _finishLine.Reached += OnFinishReached;
      _car.Died += OnCarDied;
      _gameInput.Pressed += OnPressed;
      _gameInput.Released += OnReleased;
      _gameInput.Dragged += OnDragged;
    }

    public void Exit()
    {
      _finishLine.Reached -= OnFinishReached;
      _car.Died -= OnCarDied;
      _gameInput.Pressed -= OnPressed;
      _gameInput.Released -= OnReleased;
      _gameInput.Dragged -= OnDragged;

      _turret.StopFiring();
    }

    private void OnFinishReached() => _appStateMachine.Enter<LevelWinState>();

    private void OnCarDied() => _appStateMachine.Enter<LevelLoseState>();

    private void OnPressed() => _turret.StartFiring();

    private void OnReleased() => _turret.StopFiring();

    private void OnDragged(float screenWidths) => _turret.Aim(screenWidths);
  }
}
