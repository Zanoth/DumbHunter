using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class MonsterJsonConverter : JsonConverter<Monster>, IJsonConverter
{
  public override Monster Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = MonsterId.New(root.GetProperty(nameof(Monster.MonsterId)).GetString());
    var name = root.GetProperty(nameof(Monster.Name)).GetString();

    var ecologyStr = root.GetProperty(nameof(Monster.Ecology)).GetString();
    var ecology = Enum.Parse<MonsterEcology>(ecologyStr);

    var sizeStr = root.GetProperty(nameof(Monster.Size)).GetString();
    var size = Enum.Parse<MonsterSize>(sizeStr);
    
    var pitFallTrap = root.GetProperty(nameof(Monster.TrapInteractions.PitfallTrap)).GetBoolean();
    var shockTrap = root.GetProperty(nameof(Monster.TrapInteractions.ShockTrap)).GetBoolean();
    var vineTrap = root.GetProperty(nameof(Monster.TrapInteractions.VineTrap)).GetBoolean();
    var trapInteractions = new TrapInteractions(pitFallTrap, shockTrap, vineTrap);

    var drops = new List<Drop>();

    var dropsArray = root.GetProperty(nameof(Monster.Drops)).EnumerateArray();
    while (dropsArray.MoveNext())
    {
      var dropJson = dropsArray.Current;

      var drop = JsonSerializer.Deserialize<Drop>(dropJson.GetRawText(), options);
      drops.Add(drop);
    }

    var monster = new Monster(id, name, ecology, size, trapInteractions, drops);
    return monster;
  }

  public override void Write(Utf8JsonWriter writer, Monster monster, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(Monster.MonsterId), monster.MonsterId.Id);
    writer.WriteString(nameof(Monster.Name), monster.Name);
    writer.WriteString(nameof(Monster.Ecology), monster.Ecology.ToString());
    writer.WriteString(nameof(Monster.Size), monster.Size.ToString());
    writer.WriteBoolean(nameof(Monster.TrapInteractions.PitfallTrap), monster.TrapInteractions.PitfallTrap);
    writer.WriteBoolean(nameof(Monster.TrapInteractions.ShockTrap), monster.TrapInteractions.ShockTrap);
    writer.WriteBoolean(nameof(Monster.TrapInteractions.VineTrap), monster.TrapInteractions.VineTrap);
    
    writer.WriteStartArray(nameof(Monster.Drops));
    foreach (var drop in monster.Drops)
    {
      JsonSerializer.Serialize(writer, drop, options);
    }
    writer.WriteEndArray();

    writer.WriteEndObject();
  }
}