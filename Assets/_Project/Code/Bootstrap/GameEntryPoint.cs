using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FacturaCar.Capabilities.AppFlowCapability;
using FacturaCar.Features.GameplayFeature;
using VContainer.Unity;

namespace FacturaCar.Bootstrap
{
  public class GameEntryPoint : IAsyncStartable
  {
    private readonly IAppStateMachine _appStateMachine;

    public GameEntryPoint(IAppStateMachine appStateMachine)
    {
      _appStateMachine = appStateMachine;
    }

    public async UniTask StartAsync(CancellationToken cancellation)
    {
      try
      {
        await _appStateMachine.Enter<LevelReadyState>();
      }
      catch (OperationCanceledException)
      {
      }
    }
  }
}
