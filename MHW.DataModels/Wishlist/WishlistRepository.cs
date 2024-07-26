using Serialization.Abstraction;
using SharedDataModels.Abstractions;

namespace MHW.DataModels.Wishlist;

public class WishlistRepository : RepositoryBase<IGearId, EntityTracker>
{
  public WishlistRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(WishlistRepository).Namespace}.WishlistConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<IGearId, EntityTracker> entityDictionary, List<EntityTracker> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add((IGearId) entity.Id, entity);
  }
}