using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelReadyState : IState
  {
    private readonly IAppStateMachine _appStateMachine;
    private readonly Car _car;
    private readonly Turret _turret;
    private readonly EnemySpawner _enemySpawner;
    private readonly GameInput _gameInput;

    public LevelReadyState(
      IAppStateMachine appStateMachine,
      Car car,
      Turret turret,
      EnemySpawner enemySpawner,
      GameInput gameInput)
    {
      _appStateMachine = appStateMachine;
      _car = car;
      _turret = turret;
      _enemySpawner = enemySpawner;
      _gameInput = gameInput;
    }

    public void Enter()
    {
      _car.ResetToStart();
      _turret.ResetAim();
      _enemySpawner.Respawn();
      _gameInput.Pressed += OnPressed;
    }

    public void Exit() => _gameInput.Pressed -= OnPressed;

    private void OnPressed() => _appStateMachine.Enter<LevelPlayState>();
  }
}
