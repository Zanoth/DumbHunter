using SharedDataModels.Abstractions.Decorations;
using SharedDataModels.Abstractions.Gear.Charms;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class CharmJsonConverter : JsonConverter<Charm>, IJsonConverter
{
  public override Charm Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = CharmId.New(root.GetProperty(nameof(Charm.CharmId)).ToString());
    var name = root.GetProperty(nameof(Charm.Name)).ToString();
    var rarity = root.GetProperty(nameof(Charm.Rarity)).GetInt32();

    var skills = new List<GearSkill>();
    var skillsArray = root.GetProperty(nameof(Charm.Skills)).EnumerateArray();
    while (skillsArray.MoveNext())
    {
      var skillJson = skillsArray.Current;

      var charmSkill = JsonSerializer.Deserialize<GearSkill>(skillJson.GetRawText(), options);
      skills.Add(charmSkill);
    }

    var charm = new Charm(id, name, rarity, skills);
    return charm;
  }

  public override void Write(Utf8JsonWriter writer, Charm Charm, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(Charm.CharmId), Charm.CharmId.Id);
    writer.WriteString(nameof(Charm.Name), Charm.Name);
    writer.WriteNumber(nameof(Charm.Rarity), Charm.Rarity);

    writer.WriteStartArray(nameof(Charm.Skills));
    foreach (var skill in Charm.Skills) 
      JsonSerializer.Serialize(writer, skill, options);
    writer.WriteEndArray();

    writer.WriteEndObject();
  }
}