using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelLoseState : IState
  {
    private readonly IAppStateMachine _appStateMachine;
    private readonly Car _car;
    private readonly GameplayMediator _gameplayMediator;
    private readonly GameInput _gameInput;

    public LevelLoseState(
      IAppStateMachine appStateMachine,
      Car car,
      GameplayMediator gameplayMediator,
      GameInput gameInput)
    {
      _appStateMachine = appStateMachine;
      _car = car;
      _gameplayMediator = gameplayMediator;
      _gameInput = gameInput;
    }

    public void Enter()
    {
      _car.Stop();
      _gameplayMediator.ShowLose();
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
