namespace SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

public class KinsectMechanics : IWeaponMechanic
{
  public KinsectMechanics(KinsectBonusType kinsectBonusType)
  {
    KinsectBonusType = kinsectBonusType;
  }

  public KinsectBonusType KinsectBonusType { get; init; }
  public string TEMP_ToString() => KinsectBonusType.ToString();
}