using FacturaCar.Capabilities.AppFlowCapability;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Bootstrap
{
  public class GameScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      new AppFlowInstaller().Install(builder);

      builder.RegisterEntryPoint<GameEntryPoint>();
    }
  }
}
