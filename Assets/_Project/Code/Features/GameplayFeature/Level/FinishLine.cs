using System;
using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class FinishLine : MonoBehaviour
  {
    public event Action Reached;

    public void PlaceAt(float distance) => transform.position = Vector3.forward * distance;

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out Car _))
        Reached?.Invoke();
    }
  }
}
