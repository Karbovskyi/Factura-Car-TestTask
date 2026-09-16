namespace FacturaCar.Features.GameplayFeature
{
  public interface ITurretConfig
  {
    float MaxAngle { get; }
    float DegreesPerScreenWidth { get; }
    float RotationSpeed { get; }
    float FireRate { get; }
  }
}
