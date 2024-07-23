using Serialization.Abstraction;
using SharedDataModels.Abstractions.Locations;

namespace MHW.DataModels.Locations;

public class LocationRepository : RepositoryBase<LocationId, Location>
{
  public LocationRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(LocationRepository).Namespace}.LocationConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<LocationId, Location> entityDictionary, List<Location> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.LocationId, entity);
  }
}