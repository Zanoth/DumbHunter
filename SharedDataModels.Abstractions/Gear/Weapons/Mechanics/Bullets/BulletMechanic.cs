using SharedDataModels.Abstractions.Gear.Ammos;

namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Bullets;

public class BulletMechanic : IWeaponMechanic
{
  public BulletMechanic(int deviation, SpecialAmmoType special, List<Ammo> supportedAmmo)
  {
    Deviation = deviation;
    Special = special;
    SupportedAmmo = supportedAmmo;
  }

  public int Deviation { get; init; }
  public SpecialAmmoType Special { get; init; }
  public List<Ammo> SupportedAmmo { get; init; }
}