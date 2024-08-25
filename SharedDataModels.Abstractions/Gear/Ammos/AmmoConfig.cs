namespace SharedDataModels.Abstractions.Gear.Ammos;

public class Ammo
{
  public Ammo(AmmoType ammoType, int clipSize, bool rapidFire, int recoil, RelodSpeed reloadSpeed)
  {
    AmmoType = ammoType;
    ClipSize = clipSize;
    RapidFire = rapidFire;
    Recoil = recoil;
    ReloadSpeed = reloadSpeed;
  }

  public AmmoType AmmoType { get; init; }
  public int ClipSize { get; init; }
  public bool RapidFire { get; init; }
  public int Recoil { get; init; }
  public RelodSpeed ReloadSpeed { get; init; }
}