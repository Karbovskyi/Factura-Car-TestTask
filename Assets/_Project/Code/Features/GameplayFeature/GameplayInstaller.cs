using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameplayInstaller : IInstaller
  {
    private readonly CarConfig _carConfig;
    private readonly TurretConfig _turretConfig;
    private readonly BulletConfig _bulletConfig;
    private readonly LevelConfig _levelConfig;
    private readonly CameraFollow _cameraFollow;
    private readonly FinishLine _finishLine;
    private readonly WinView _winView;

    public GameplayInstaller(
      CarConfig carConfig,
      TurretConfig turretConfig,
      BulletConfig bulletConfig,
      LevelConfig levelConfig,
      CameraFollow cameraFollow,
      FinishLine finishLine,
      WinView winView)
    {
      _carConfig = carConfig;
      _turretConfig = turretConfig;
      _bulletConfig = bulletConfig;
      _levelConfig = levelConfig;
      _cameraFollow = cameraFollow;
      _finishLine = finishLine;
      _winView = winView;
    }

    public void Install(IContainerBuilder builder)
    {
      builder.RegisterInstance<ICarConfig>(_carConfig);
      builder.RegisterInstance<ITurretConfig>(_turretConfig);
      builder.RegisterInstance<IBulletConfig>(_bulletConfig);
      builder.RegisterInstance<ILevelConfig>(_levelConfig);
      builder.RegisterComponent(_cameraFollow);
      builder.RegisterComponent(_finishLine);
      builder.RegisterComponent(_winView);

      builder.Register<GameInput>(Lifetime.Scoped);
      builder.Register<GroundFactory>(Lifetime.Scoped);
      builder.Register<BulletFactory>(Lifetime.Scoped);
      builder.Register<CarFactory>(Lifetime.Scoped);
      builder.Register(resolver => resolver.Resolve<CarFactory>().Create(), Lifetime.Scoped);
      builder.Register(resolver => resolver.Resolve<Car>().Turret, Lifetime.Scoped);

      builder.Register<LevelLoadState>(Lifetime.Scoped);
      builder.Register<LevelReadyState>(Lifetime.Scoped);
      builder.Register<LevelPlayState>(Lifetime.Scoped);
      builder.Register<LevelWinState>(Lifetime.Scoped);
    }
  }
}
