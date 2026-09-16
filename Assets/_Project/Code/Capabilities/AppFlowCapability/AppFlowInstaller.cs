using VContainer;
using VContainer.Unity;

namespace FacturaCar.Capabilities.AppFlowCapability
{
  public class AppFlowInstaller : IInstaller
  {
    public void Install(IContainerBuilder builder)
    {
      builder.Register<IAppStateMachine, AppStateMachine>(Lifetime.Singleton);
    }
  }
}
