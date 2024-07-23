using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class SkillLevelJsonConverter : JsonConverter<SkillLevel>, IJsonConverter
{
  public override SkillLevel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var level = root.GetProperty(nameof(SkillLevel.Level)).GetInt32();
    var description = root.GetProperty(nameof(SkillLevel.Description)).GetString();

    return new SkillLevel(level, description);
  }

  public override void Write(Utf8JsonWriter writer, SkillLevel value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteNumber(nameof(SkillLevel.Level), value.Level);
    writer.WriteString(nameof(SkillLevel.Description), value.Description);
    writer.WriteEndObject();
  }
}