using Serialization.Abstraction;

namespace MHW.DataModels.Skills;

public class SkillRepository : RepositoryBase<SkillId, Skill>
{
  private static readonly string ResourceName = $"{typeof(SkillRepository).Namespace}.SkillConfiguration.json";

  public SkillRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<SkillId, Skill> entityDictionary, List<Skill> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.SkillId, entity);
  }
}