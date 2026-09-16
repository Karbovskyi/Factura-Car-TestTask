using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class WinView : MonoBehaviour
  {
    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);
  }
}
