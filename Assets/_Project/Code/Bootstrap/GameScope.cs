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
    [SerializeField] private TurretConfig _turretConfig;
    [SerializeField] private BulletConfig _bulletConfig;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private FinishLine _finishLine;
    [SerializeField] private WinView _winView;

    protected override void Configure(IContainerBuilder builder)
    {
      new AppFlowInstaller().Install(builder);
      new GameplayInstaller(
          _carConfig,
          _turretConfig,
          _bulletConfig,
          _levelConfig,
          _cameraFollow,
          _finishLine,
          _winView)
        .Install(builder);

      builder.RegisterEntryPoint<GameEntryPoint>();
    }
  }
}
