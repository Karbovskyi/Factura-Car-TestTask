using UnityEngine;

namespace FacturaCar.Features.GameplayFeature
{
  public class CameraFollow : MonoBehaviour
  {
    private Transform _target;
    private Vector3 _offset;

    public void Follow(Transform target)
    {
      _target = target;
      _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
      if (_target == null)
        return;

      transform.position = _target.position + _offset;
    }
  }
}
