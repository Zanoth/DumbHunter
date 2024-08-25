using Serialization.Abstraction;

namespace MHW.DataModels.Monsters;

public class MonsterRepository : RepositoryBase<MonsterId, Monster>
{
  private static readonly string ResourceName = $"{typeof(MonsterRepository).Namespace}.MonsterConfiguration.json";

  public MonsterRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<MonsterId, Monster> entityDictionary, List<Monster> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.MonsterId, entity);
  }
}