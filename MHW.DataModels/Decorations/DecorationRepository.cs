using Serialization.Abstraction;

namespace MHW.DataModels.Decorations;

public class DecorationRepository : RepositoryBase<DecorationId, Decoration>
{
  public DecorationRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(DecorationRepository).Namespace}.DecorationConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<DecorationId, Decoration> entityDictionary, List<Decoration> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.DecorationId, entity);
  }
}