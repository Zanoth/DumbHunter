using Serialization.Abstraction;
using SharedDataModels.Abstractions;

namespace MHW.DataModels.Inventory;

public class InventoryRepository : RepositoryBase<IStrongId, EntityTracker>
{
  private static readonly string ResourceName = $"{typeof(InventoryRepository).Namespace}.InventoryConfiguration.json";

  public InventoryRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<IStrongId, EntityTracker> entityDictionary, List<EntityTracker> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add((DecorationId)entity.Id, entity);
  }
}