using SharedDataModels.Abstractions.Locations;

namespace SharedDataModels.Abstractions.Quests;

public record Quest(
  QuestId QuestId,
  string Name,
  QuestCategory Category,
  QuestRank Rank,
  int Stars,
  LocationId LocationId,
  List<EntityTracker> Monsters,
  List<QuestObjective> Objectives,
  QuestRewards Rewards);