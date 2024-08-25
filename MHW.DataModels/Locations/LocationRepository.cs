using Serialization.Abstraction;
using SharedDataModels.Abstractions.Locations;

namespace MHW.DataModels.Locations;

public class LocationRepository : RepositoryBase<LocationId, Location>
{
  private static readonly string ResourceName = $"{typeof(LocationRepository).Namespace}.LocationConfiguration.json";

  public LocationRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<LocationId, Location> entityDictionary, List<Location> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.LocationId, entity);
  }
}