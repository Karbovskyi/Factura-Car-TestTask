using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "LevelConfig", menuName = "Factura Car/Level Config")]
  public class LevelConfig : ScriptableObject, ILevelConfig
  {
    [SerializeField, Min(0f)] private float _length;
    [SerializeField] private GameObject _groundSegmentPrefab;

    public float Length => _length;
    public GameObject GroundSegmentPrefab => _groundSegmentPrefab;
  }
}
