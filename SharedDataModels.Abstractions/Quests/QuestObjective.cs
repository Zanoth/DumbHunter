namespace SharedDataModels.Abstractions.Quests;

public class QuestObjective
{
    public QuestObjective(QuestType type, List<QuestEntityTracker> requirements)
    {
        QuestType = type;
        Requirements = requirements;
    }

    public QuestType QuestType { get; init; }
    public List<QuestEntityTracker> Requirements { get; init; } = new();
}