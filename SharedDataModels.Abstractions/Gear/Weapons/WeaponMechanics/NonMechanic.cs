namespace SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

public class NonMechanic : IWeaponMechanic
{
  public string Description => "Mechanic used for weapons without stats-extending mechanics";
  public string TEMP_ToString() => $"{nameof(NonMechanic)}: {Description}";
}