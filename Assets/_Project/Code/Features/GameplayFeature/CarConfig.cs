using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  [CreateAssetMenu(fileName = "CarConfig", menuName = "Factura Car/Car Config")]
  public class CarConfig : ScriptableObject, ICarConfig
  {
    [SerializeField] private GameObject _prefab;

    public GameObject Prefab => _prefab;
  }
}
