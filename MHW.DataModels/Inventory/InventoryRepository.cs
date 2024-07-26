using Serialization.Abstraction;
using SharedDataModels.Abstractions;

namespace MHW.DataModels.Inventory;

public class InventoryRepository : RepositoryBase<IStrongId, EntityTracker>
{
  public InventoryRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(InventoryRepository).Namespace}.InventoryConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<IStrongId, EntityTracker> entityDictionary, List<EntityTracker> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add((DecorationId)entity.Id, entity);
  }
}