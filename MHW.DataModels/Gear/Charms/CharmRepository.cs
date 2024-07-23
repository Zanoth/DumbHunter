using Serialization.Abstraction;
using SharedDataModels.Abstractions.Gear.Charms;

namespace MHW.DataModels.Gear.Charms;
public class CharmRepository : RepositoryBase<CharmId, Charm>
{
  public CharmRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(CharmRepository).Namespace}.CharmConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<CharmId, Charm> entityDictionary, List<Charm> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.CharmId, entity);
  }
}