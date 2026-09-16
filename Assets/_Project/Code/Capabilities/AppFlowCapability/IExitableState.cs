using Cysharp.Threading.Tasks;

namespace FacturaCar.Capabilities.AppFlowCapability
{
  public interface IExitableState
  {
    UniTask ExitAsync();
  }
}
