namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Kinsects;

public interface IKinsectWeapon : ISharpnessWeapon
{
  public KinsectMechanic KinsectMechanic { get; }
}