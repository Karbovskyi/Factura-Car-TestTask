using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameplayInstaller : IInstaller
  {
    private readonly CarConfig _carConfig;
    private readonly LevelConfig _levelConfig;

    public GameplayInstaller(CarConfig carConfig, LevelConfig levelConfig)
    {
      _carConfig = carConfig;
      _levelConfig = levelConfig;
    }

    public void Install(IContainerBuilder builder)
    {
      builder.RegisterInstance<ICarConfig>(_carConfig);
      builder.RegisterInstance<ILevelConfig>(_levelConfig);
      builder.Register<CarFactory>(Lifetime.Scoped);
      builder.Register<GroundFactory>(Lifetime.Scoped);
    }
  }
}
