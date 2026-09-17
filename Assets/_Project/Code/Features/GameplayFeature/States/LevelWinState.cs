using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelWinState : IState
  {
    private readonly IAppStateMachine _appStateMachine;
    private readonly Car _car;
    private readonly EnemySpawner _enemySpawner;
    private readonly GameplayMediator _gameplayMediator;
    private readonly GameInput _gameInput;

    public LevelWinState(
      IAppStateMachine appStateMachine,
      Car car,
      EnemySpawner enemySpawner,
      GameplayMediator gameplayMediator,
      GameInput gameInput)
    {
      _appStateMachine = appStateMachine;
      _car = car;
      _enemySpawner = enemySpawner;
      _gameplayMediator = gameplayMediator;
      _gameInput = gameInput;
    }

    public void Enter()
    {
      _car.Stop();
      _enemySpawner.StopAll();
      _gameplayMediator.ShowWin();
      _gameInput.Pressed += OnPressed;
    }

    public void Exit()
    {
      _gameInput.Pressed -= OnPressed;
      _gameplayMediator.HideResult();
    }

    private void OnPressed() => _appStateMachine.Enter<LevelReadyState>();
  }
}
