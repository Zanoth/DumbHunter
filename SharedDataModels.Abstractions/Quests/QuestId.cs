namespace SharedDataModels.Abstractions.Quests;

public record QuestId(string Id) : IStrongId<string>
{
  public static QuestId Empty() => new(string.Empty);
  public static QuestId New(string idStr) => new(idStr);
}