using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class ArmorJsonConverter : JsonConverter<Armor>, IJsonConverter
{
  public override Armor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = ArmorId.New(root.GetProperty(nameof(Armor.ArmorId)).GetString());
    var name = root.GetProperty(nameof(Armor.Name)).GetString();
    var rarity = root.GetProperty(nameof(Armor.Rarity)).GetInt32();
    var armorType = (ArmorType)Enum.Parse(typeof(ArmorType), root.GetProperty(nameof(Armor.ArmorType)).GetString());
    var defenseStats = JsonSerializer.Deserialize<DefenseStats>(root.GetProperty(nameof(Armor.DefenseStats)).GetRawText());

    var skills = new List<GearSkill>();
    var decoSlots = new List<DecorationSlot>();

    var skillsArray = root.GetProperty(nameof(Armor.Skills)).EnumerateArray();
    while (skillsArray.MoveNext())
    {
      var skillJson = skillsArray.Current;
      var gearSkill = JsonSerializer.Deserialize<GearSkill>(skillJson.GetRawText(), options);
      skills.Add(gearSkill);
    }
    var decoSlotArray = root.GetProperty(nameof(Armor.DecorationSlots)).EnumerateArray();
    while (decoSlotArray.MoveNext())
    {
      var decoSlotJson = decoSlotArray.Current;

      var slotLevel = decoSlotJson.GetProperty(nameof(DecorationSlot.SlotLevel)).GetInt32();

      var decoSlot = new DecorationSlot(slotLevel, DecorationId.Empty());
      decoSlots.Add(decoSlot);
    }

    var armor = new Armor(id, name, rarity, armorType, skills, decoSlots, defenseStats);
    return armor;
  }

  public override void Write(Utf8JsonWriter writer, Armor value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(Armor.ArmorId), value.ArmorId.Id);
    writer.WriteString(nameof(Armor.Name), value.Name);
    writer.WriteNumber(nameof(Armor.Rarity), value.Rarity);
    writer.WriteString(nameof(Armor.ArmorType), value.ArmorType.ToString());

    writer.WritePropertyName(nameof(Armor.Skills));
    JsonSerializer.Serialize(writer, value.Skills, options);

    writer.WritePropertyName(nameof(Armor.DecorationSlots));
    JsonSerializer.Serialize(writer, value.DecorationSlots, options);

    writer.WritePropertyName(nameof(Armor.DefenseStats));
    JsonSerializer.Serialize(writer, value.DefenseStats, options);

    writer.WriteEndObject();
  }
}