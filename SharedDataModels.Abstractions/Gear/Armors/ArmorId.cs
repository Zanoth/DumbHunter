namespace SharedDataModels.Abstractions.Gear.Armors;

public record ArmorId(string Id) : IGearId
{
  public static ArmorId Empty() => new(string.Empty);
  public static ArmorId New(string idStr) => new(idStr);

}