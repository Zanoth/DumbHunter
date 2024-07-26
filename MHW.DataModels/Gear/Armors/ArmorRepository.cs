using Serialization.Abstraction;

namespace MHW.DataModels.Gear.Armors;

public class ArmorRepository : RepositoryBase<ArmorId, Armor>
{
  public ArmorRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(ArmorRepository).Namespace}.ArmorConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<ArmorId, Armor> entityDictionary, List<Armor> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.ArmorId, entity);
  }
}