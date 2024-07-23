namespace SharedDataModels.Abstractions.Locations;

public record LocationId(string Id) : IStrongId<string>
{
  public static LocationId Empty() => new(string.Empty);
  public static LocationId New(string idStr) => new(idStr);
}