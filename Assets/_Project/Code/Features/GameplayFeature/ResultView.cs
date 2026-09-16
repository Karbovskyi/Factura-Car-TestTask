using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class ResultView : MonoBehaviour
  {
    [SerializeField] private GameObject _winTitle;
    [SerializeField] private GameObject _loseTitle;

    public void ShowWin() => Show(isWin: true);

    public void ShowLose() => Show(isWin: false);

    public void Hide() => gameObject.SetActive(false);

    private void Show(bool isWin)
    {
      _winTitle.SetActive(isWin);
      _loseTitle.SetActive(!isWin);
      gameObject.SetActive(true);
    }
  }
}
