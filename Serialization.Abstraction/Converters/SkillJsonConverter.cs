using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class SkillJsonConverter : JsonConverter<Skill>, IJsonConverter
{
  public override Skill Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = root.GetProperty(nameof(Skill.SkillId)).ToString();
    var name = root.GetProperty(nameof(Skill.Name)).ToString();
    var iconColor = Color.FromName(root.GetProperty(nameof(Skill.IconColor)).ToString());
    var secret = root.GetProperty(nameof(Skill.Secret)).GetInt32();
    var unlocksId = root.GetProperty(nameof(Skill.UnlocksId)).ToString();


    var levels = new List<SkillLevel>();
    var levelArray = root.GetProperty(nameof(Skill.Levels)).EnumerateArray();
    while (levelArray.MoveNext())
    {
      var levelJson = levelArray.Current;

      var level = levelJson.GetProperty(nameof(SkillLevel.Level)).GetInt32();
      var description = levelJson.GetProperty(nameof(SkillLevel.Description)).GetString();

      var skillLevel = new SkillLevel(level, description);
      levels.Add(skillLevel);
    }

    var skill = new Skill(SkillId.New(id), name, iconColor, secret, SkillId.New(unlocksId), levels);
    return skill;
  }

  public override void Write(Utf8JsonWriter writer, Skill skill, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(Skill.SkillId), skill.SkillId.Id);
    writer.WriteString(nameof(Skill.Name), skill.Name);
    writer.WriteString(nameof(Skill.IconColor), skill.IconColor.Name);
    writer.WriteNumber(nameof(Skill.Secret), skill.Secret);
    writer.WriteString(nameof(Skill.UnlocksId), skill.UnlocksId.Id);

    writer.WritePropertyName(nameof(Skill.Levels));
    writer.WriteStartArray();
    foreach (var level in skill.Levels)
    {
      writer.WriteStartObject();
      writer.WriteNumber(nameof(SkillLevel.Level), level.Level);
      writer.WriteString(nameof(SkillLevel.Description), level.Description);
      writer.WriteEndObject();
    }
    writer.WriteEndArray();

    writer.WriteEndObject();
  }
}