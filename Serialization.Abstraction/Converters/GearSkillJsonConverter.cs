using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class GearSkillJsonConverter : JsonConverter<GearSkill>, IJsonConverter
{
  private readonly Func<IRepository<SkillId, Skill>> _repositoryAccessor;
  public GearSkillJsonConverter(IRepositoryFactory<SkillId, Skill> repositoryFactory) => 
    _repositoryAccessor = () => repositoryFactory.CreateRepository();

  public override GearSkill Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = SkillId.New(root.GetProperty(nameof(Skill.SkillId)).ToString());
    var level = root.GetProperty(nameof(GearSkill.SkillLevel)).GetInt32();

    var skill = _repositoryAccessor().Get(id);

    var gearSkill = new GearSkill(skill, level);
    return gearSkill;
  }

  public override void Write(Utf8JsonWriter writer, GearSkill gearSkill, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(Skill.SkillId), gearSkill.Skill.SkillId.Id);
    writer.WriteNumber(nameof(GearSkill.SkillLevel), gearSkill.SkillLevel);
    writer.WriteEndObject();
  }
}