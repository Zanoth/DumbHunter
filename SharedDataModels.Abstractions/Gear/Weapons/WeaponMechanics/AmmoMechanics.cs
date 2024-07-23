namespace SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

//Todo: Add AmmoConfiguration.json to the project
//Refactor: Match this up with the actual AmmoConfiguration.json
public class AmmoMechanics : IWeaponMechanic
{
  public string AmmoConfigId { get; init; }
  public string TEMP_ToString() => AmmoConfigId;
}