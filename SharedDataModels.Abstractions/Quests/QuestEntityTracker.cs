namespace SharedDataModels.Abstractions.Quests;

public class QuestEntityTracker
{
    public IStrongId<string> Id { get; set; }
    public int Count { get; set; }

    public QuestEntityTracker(IStrongId<string> id, int count)
    {
        Id = id;
        Count = count;
    }

    public void Deconstruct(out IStrongId<string> id, out int quantity)
    {
      id = Id;
      quantity = Count;
    }
}