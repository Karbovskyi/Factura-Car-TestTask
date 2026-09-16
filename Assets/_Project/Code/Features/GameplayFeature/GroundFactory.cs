using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FacturaCar.Features.GameplayFeature
{
  public class GroundFactory
  {
    private readonly IObjectResolver _resolver;
    private readonly ILevelConfig _config;

    public GroundFactory(IObjectResolver resolver, ILevelConfig config)
    {
      _resolver = resolver;
      _config = config;
    }

    public void Create()
    {
      Transform ground = new GameObject("Ground").transform;
      float segmentLength = _config.GroundSegmentPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
      int segmentsToFinish = Mathf.CeilToInt(_config.Length / segmentLength);

      for (int i = 0; i <= segmentsToFinish; i++)
        CreateSegment(ground, i * segmentLength);
    }

    private void CreateSegment(Transform ground, float distance)
    {
      GameObject segment = _resolver.Instantiate(_config.GroundSegmentPrefab, ground);
      segment.transform.localPosition = new Vector3(0f, 0f, distance);
    }
  }
}
