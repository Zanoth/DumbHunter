namespace SharedDataModels.Abstractions.Decorations;
public record DecorationId(string Id) : IStrongId<string>
{
  public static DecorationId Empty() => new(string.Empty);
  public static DecorationId New(string idStr) => new(idStr);
}