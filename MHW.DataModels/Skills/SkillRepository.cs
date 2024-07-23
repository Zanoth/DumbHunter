using Serialization.Abstraction;

namespace MHW.DataModels.Skills;

public class SkillRepository : RepositoryBase<SkillId, Skill>
{
  public SkillRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(SkillRepository).Namespace}.SkillConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<SkillId, Skill> entityDictionary, List<Skill> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.SkillId, entity);
  }
}