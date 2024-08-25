using Serialization.Abstraction;
using SharedDataModels.Abstractions.Gear.Kinsect;

namespace MHW.DataModels.Gear.Kinsects;

public class KinsectRepository : RepositoryBase<KinsectId, Kinsect>
{
  private static readonly string ResourceName = $"{typeof(KinsectRepository).Namespace}.KinsectConfiguration.json";

  public KinsectRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<KinsectId, Kinsect> entityDictionary, List<Kinsect> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.KinsectId, entity);
  }
}