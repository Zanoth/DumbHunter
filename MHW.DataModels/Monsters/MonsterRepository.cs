using Serialization.Abstraction;

namespace MHW.DataModels.Monsters;

public class MonsterRepository : RepositoryBase<MonsterId, Monster>
{
  public MonsterRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(MonsterRepository).Namespace}.MonsterConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<MonsterId, Monster> entityDictionary, List<Monster> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.MonsterId, entity);
  }
}