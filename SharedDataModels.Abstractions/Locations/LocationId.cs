namespace SharedDataModels.Abstractions.Locations;

public record LocationId(string Id) : IStrongId
{
  public static LocationId Empty() => new(string.Empty);
  public static LocationId New(string idStr) => new(idStr);
}