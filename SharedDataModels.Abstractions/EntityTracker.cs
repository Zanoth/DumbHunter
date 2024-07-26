namespace SharedDataModels.Abstractions;

public class EntityTracker
{
  public IStrongId Id { get; set; }
  public int Count { get; set; }

  public EntityTracker(IStrongId id, int count)
  {
    Id = id;
    Count = count;
  }

  public void Deconstruct(out IStrongId id, out int quantity)
  {
    id = Id;
    quantity = Count;
  }
}