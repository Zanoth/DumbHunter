using Serialization.Abstraction;
using SharedDataModels.Abstractions.Gear.Charms;

namespace MHW.DataModels.Gear.Charms;
public class CharmRepository : RepositoryBase<CharmId, Charm>
{
  private static readonly string ResourceName = $"{typeof(CharmRepository).Namespace}.CharmConfiguration.json";

  public CharmRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<CharmId, Charm> entityDictionary, List<Charm> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.CharmId, entity);
  }
}