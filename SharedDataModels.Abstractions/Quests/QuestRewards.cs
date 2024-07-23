using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions.Quests;

public class QuestRewards
{
  public QuestRewards(int zenny, List<LootDetails> items)
  {
    Zenny = zenny;
    Items = items;
  }

  public int Zenny { get; init; } = 0;
  public List<LootDetails> Items { get; init; } = new();
}