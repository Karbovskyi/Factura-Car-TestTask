using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameplayInstaller : IInstaller
  {
    private readonly CarConfig _carConfig;
    private readonly TurretConfig _turretConfig;
    private readonly BulletConfig _bulletConfig;
    private readonly EnemyConfig _enemyConfig;
    private readonly LevelConfig _levelConfig;
    private readonly CarCamera _carCamera;
    private readonly FinishLine _finishLine;
    private readonly ResultView _resultView;

    public GameplayInstaller(
      CarConfig carConfig,
      TurretConfig turretConfig,
      BulletConfig bulletConfig,
      EnemyConfig enemyConfig,
      LevelConfig levelConfig,
      CarCamera carCamera,
      FinishLine finishLine,
      ResultView resultView)
    {
      _carConfig = carConfig;
      _turretConfig = turretConfig;
      _bulletConfig = bulletConfig;
      _enemyConfig = enemyConfig;
      _levelConfig = levelConfig;
      _carCamera = carCamera;
      _finishLine = finishLine;
      _resultView = resultView;
    }

    public void Install(IContainerBuilder builder)
    {
      builder.RegisterInstance<ICarConfig>(_carConfig);
      builder.RegisterInstance<ITurretConfig>(_turretConfig);
      builder.RegisterInstance<IBulletConfig>(_bulletConfig);
      builder.RegisterInstance<IEnemyConfig>(_enemyConfig);
      builder.RegisterInstance<ILevelConfig>(_levelConfig);
      builder.RegisterComponent(_carCamera);
      builder.RegisterComponent(_finishLine);
      builder.RegisterComponent(_resultView);

      builder.Register<GameInput>(Lifetime.Scoped);
      builder.Register<GroundFactory>(Lifetime.Scoped);
      builder.Register<BulletFactory>(Lifetime.Scoped);
      builder.Register<EnemyFactory>(Lifetime.Scoped);
      builder.Register<EnemySplatFactory>(Lifetime.Scoped);
      builder.Register<DamageNumberFactory>(Lifetime.Scoped);
      builder.Register<EnemySpawner>(Lifetime.Scoped);
      builder.Register<CarFactory>(Lifetime.Scoped);
      builder.Register(resolver => resolver.Resolve<CarFactory>().Create(), Lifetime.Scoped);
      builder.Register(resolver => resolver.Resolve<Car>().Turret, Lifetime.Scoped);
      builder.Register(resolver => resolver.Resolve<Car>().GetComponentInChildren<HealthBarView>(), Lifetime.Scoped);
      builder.RegisterEntryPoint<GameplayMediator>().AsSelf();

      builder.Register<LevelLoadState>(Lifetime.Scoped);
      builder.Register<LevelReadyState>(Lifetime.Scoped);
      builder.Register<LevelPlayState>(Lifetime.Scoped);
      builder.Register<LevelWinState>(Lifetime.Scoped);
      builder.Register<LevelLoseState>(Lifetime.Scoped);
    }
  }
}
