using Serialization.Abstraction;

namespace MHW.DataModels.Decorations;

public class DecorationRepository : RepositoryBase<DecorationId, Decoration>
{
  private static readonly string ResourceName = $"{typeof(DecorationRepository).Namespace}.DecorationConfiguration.json";

  public DecorationRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<DecorationId, Decoration> entityDictionary, List<Decoration> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.DecorationId, entity);
  }
}