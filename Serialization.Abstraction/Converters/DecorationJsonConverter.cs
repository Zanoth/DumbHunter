using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class DecorationJsonConverter : JsonConverter<Decoration>, IJsonConverter
{
  public override Decoration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = DecorationId.New(root.GetProperty(nameof(Decoration.DecorationId)).ToString());
    var name = root.GetProperty(nameof(Decoration.Name)).ToString();
    var requiredSlotSize = root.GetProperty(nameof(Decoration.RequiredSlotSize)).GetInt32();
    var rarity = root.GetProperty(nameof(Decoration.Rarity)).GetInt32();
    var iconColor = Color.FromName(root.GetProperty(nameof(Decoration.IconColor)).ToString());

    //Refactor: This is repeated in several converters. Consider moving to a shared method.
    var skills = new List<GearSkill>();
    var skillsArray = root.GetProperty(nameof(Decoration.Skills)).EnumerateArray();
    while (skillsArray.MoveNext())
    {
      var skillJson = skillsArray.Current;
      var decorationSkill = JsonSerializer.Deserialize<GearSkill>(skillJson.GetRawText(), options);
      skills.Add(decorationSkill);
    }

    var decoration = new Decoration(id, name, requiredSlotSize, rarity, iconColor, skills);
    return decoration;
  }

  public override void Write(Utf8JsonWriter writer, Decoration decoration, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(decoration.DecorationId), decoration.DecorationId.Id);
    writer.WriteString(nameof(decoration.Name), decoration.Name);
    writer.WriteNumber(nameof(decoration.RequiredSlotSize), decoration.RequiredSlotSize);
    writer.WriteNumber(nameof(decoration.Rarity), decoration.Rarity);
    writer.WriteString(nameof(decoration.IconColor), decoration.IconColor.Name);

    writer.WriteStartArray(nameof(decoration.Skills));
    foreach (var skill in decoration.Skills) 
      JsonSerializer.Serialize(writer, skill, options);
    writer.WriteEndArray();

    writer.WriteEndObject();
  }
}