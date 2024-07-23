using Serialization.Abstraction;

namespace MHW.DataModels.Items;

public class ItemRepository : RepositoryBase<ItemId, Item>
{
  public ItemRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(ItemRepository).Namespace}.ItemConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<ItemId, Item> entityDictionary, List<Item> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.ItemId, entity);
  }
}