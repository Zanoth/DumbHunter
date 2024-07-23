namespace SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

public class ShellingMechanics : IWeaponMechanic
{
  public ShellingMechanics(ShellingType shellingType, int shellingLevel)
  {
    ShellingType = shellingType;
    ShellingLevel = shellingLevel;
  }

  public ShellingType ShellingType { get; init; }
  public int ShellingLevel { get; init; }
  //public int WyrmstakeLevel { get; init; }
  public string TEMP_ToString()
  {
    return $"{nameof(ShellingMechanics)}: Type: {ShellingType}, Level: {ShellingLevel}";
  }
}