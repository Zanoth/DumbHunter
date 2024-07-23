namespace SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

public class NotYetImplementedMechanic : IWeaponMechanic
{
  public string Description => "Not yet implemented.";
  public string TEMP_ToString() => $"{nameof(NotYetImplementedMechanic)}: {Description}";
}