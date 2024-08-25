using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Kinsects;

public class KinsectMechanic : IWeaponMechanic
{
  public KinsectMechanic(KinsectBonus kinsectBonus)
  {
    KinsectBonus = kinsectBonus;
  }

  public KinsectBonus KinsectBonus { get; init; }
}