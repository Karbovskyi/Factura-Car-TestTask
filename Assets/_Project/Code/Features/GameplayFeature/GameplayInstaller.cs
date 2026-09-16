using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class GameplayInstaller : IInstaller
  {
    private readonly CarConfig _carConfig;

    public GameplayInstaller(CarConfig carConfig)
    {
      _carConfig = carConfig;
    }

    public void Install(IContainerBuilder builder)
    {
      builder.RegisterInstance<ICarConfig>(_carConfig);
      builder.Register<CarFactory>(Lifetime.Scoped);
    }
  }
}
