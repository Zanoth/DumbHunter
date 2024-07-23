namespace SharedDataModels.Abstractions.Gear.Armors;

public record ArmorId(string Id) : IGearId<string>
{
  public static ArmorId Empty() => new(string.Empty);
  public static ArmorId New(string idStr) => new(idStr);

}