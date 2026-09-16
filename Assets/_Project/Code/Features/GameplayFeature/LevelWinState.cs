using FacturaCar.Capabilities.AppFlowCapability;

namespace FacturaCar.Features.GameplayFeature
{
  public class LevelWinState : IState
  {
    private readonly IAppStateMachine _appStateMachine;
    private readonly Car _car;
    private readonly WinView _winView;
    private readonly GameInput _gameInput;

    public LevelWinState(IAppStateMachine appStateMachine, Car car, WinView winView, GameInput gameInput)
    {
      _appStateMachine = appStateMachine;
      _car = car;
      _winView = winView;
      _gameInput = gameInput;
    }

    public void Enter()
    {
      _car.Stop();
      _winView.Show();
      _gameInput.Pressed += OnPressed;
    }

    public void Exit()
    {
      _gameInput.Pressed -= OnPressed;
      _winView.Hide();
    }

    private void OnPressed() => _appStateMachine.Enter<LevelReadyState>();
  }
}
