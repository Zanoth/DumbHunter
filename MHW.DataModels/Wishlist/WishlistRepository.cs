using Serialization.Abstraction;
using SharedDataModels.Abstractions;

namespace MHW.DataModels.Wishlist;

public class WishlistRepository : RepositoryBase<IGearId, EntityTracker>
{
  private static readonly string ResourceName = $"{typeof(WishlistRepository).Namespace}.WishlistConfiguration.json";

  public WishlistRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<IGearId, EntityTracker> entityDictionary, List<EntityTracker> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add((IGearId) entity.Id, entity);
  }
}