namespace SharedDataModels.Abstractions.Quests;

public class QuestObjective
{
    public QuestObjective(QuestType type, List<EntityTracker> requirements)
    {
        QuestType = type;
        Requirements = requirements;
    }

    public QuestType QuestType { get; init; }
    public List<EntityTracker> Requirements { get; init; } = new();
}