namespace SharedDataModels.Abstractions.Monsters;
public record MonsterId(string Id) : IStrongId
{
  public static MonsterId Empty() => new(string.Empty);
  public static MonsterId New(string idStr) => new(idStr);
}