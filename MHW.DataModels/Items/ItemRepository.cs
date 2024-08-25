using Serialization.Abstraction;

namespace MHW.DataModels.Items;

public class ItemRepository : RepositoryBase<ItemId, Item>
{
  private readonly static string ResourceName = $"{typeof(ItemRepository).Namespace}.ItemConfiguration.json";

  public ItemRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<ItemId, Item> entityDictionary, List<Item> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.ItemId, entity);
  }
}