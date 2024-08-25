using Serialization.Abstraction;
using SharedDataModels.Abstractions.Quests;

namespace MHW.DataModels.Quests;

public class QuestRepository : RepositoryBase<QuestId, Quest>
{
  private static readonly string ResourceName = $"{typeof(QuestRepository).Namespace}.QuestConfiguration.json";

  public QuestRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<QuestId, Quest> entityDictionary, List<Quest> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.QuestId, entity);
  }
}