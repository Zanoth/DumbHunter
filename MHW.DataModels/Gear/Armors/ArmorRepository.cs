using MHW.DataModels.Gear.Weapons;
using Serialization.Abstraction;

namespace MHW.DataModels.Gear.Armors;

public class ArmorRepository : RepositoryBase<ArmorId, Armor>
{
  private static readonly string ResourceName = $"{typeof(ArmorRepository).Namespace}.ArmorConfiguration.json";

  public ArmorRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<ArmorId, Armor> entityDictionary, List<Armor> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.ArmorId, entity);
  }
}