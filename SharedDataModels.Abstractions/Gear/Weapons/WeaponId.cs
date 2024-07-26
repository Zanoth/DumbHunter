namespace SharedDataModels.Abstractions.Gear.Weapons;

public record WeaponId(string Id) : IGearId
{
  public static WeaponId Empty() => new(string.Empty);
  public static WeaponId New(string idStr) => new(idStr);

}
