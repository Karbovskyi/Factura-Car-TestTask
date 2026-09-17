using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class CarFactory
  {
    private readonly IObjectResolver _resolver;
    private readonly ICarConfig _config;

    public CarFactory(IObjectResolver resolver, ICarConfig config)
    {
      _resolver = resolver;
      _config = config;
    }

    public Car Create() => _resolver.Instantiate(_config.Prefab);
  }
}
