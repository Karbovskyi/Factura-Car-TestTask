using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public interface ILevelConfig
  {
    float Length { get; }
    GameObject GroundSegmentPrefab { get; }
  }
}
