namespace SharedDataModels.Abstractions.Gear.Charms;

public record CharmId(string Id) : IGearId
{
  public static CharmId Empty() => new(string.Empty);
  public static CharmId New(string idStr) => new(idStr);
}