using FacturaCar.Capabilities.AppFlowCapability;
using FacturaCar.Features.GameplayFeature;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Bootstrap
{
  public class GameScope : LifetimeScope
  {
    [SerializeField] private CarConfig _carConfig;
    [SerializeField] private LevelConfig _levelConfig;

    protected override void Configure(IContainerBuilder builder)
    {
      new AppFlowInstaller().Install(builder);
      new GameplayInstaller(_carConfig, _levelConfig).Install(builder);

      builder.RegisterEntryPoint<GameEntryPoint>();
    }
  }
}
