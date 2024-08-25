using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class MonsterJsonConverter : JsonConverter<Monster>, IJsonConverter
{
  public override Monster Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = GetMonsterId(root);
    var name = GetName(root);
    var ecology = GetMonsterEcology(root);
    var size = GetMonsterSize(root);
    var iconPath = GetIconPath(root);
    var trapInteractions = GetTrapInteractions(root);
    var drops = GetDrops(options, root);
    var formWeaknesses = GetFormWeaknesses(root);

    var monster = new Monster(id, name, ecology, size, iconPath, trapInteractions, drops, formWeaknesses);
    return monster;
  }

  private static MonsterId GetMonsterId(JsonElement root)
  {
    var id = MonsterId.New(root.GetProperty(nameof(Monster.MonsterId)).GetString());
    return id;
  }

  private static string? GetName(JsonElement root)
  {
    var name = root.GetProperty(nameof(Monster.Name)).GetString();
    return name;
  }

  private static MonsterEcology GetMonsterEcology(JsonElement root)
  {
    var ecologyStr = root.GetProperty(nameof(Monster.Ecology)).GetString();
    var ecology = Enum.Parse<MonsterEcology>(ecologyStr);
    return ecology;
  }

  private static MonsterSize GetMonsterSize(JsonElement root)
  {
    var sizeStr = root.GetProperty(nameof(Monster.Size)).GetString();
    var size = Enum.Parse<MonsterSize>(sizeStr);
    return size;
  }

  private string GetIconPath(JsonElement root)
  {
    var iconPath = root.GetProperty(nameof(Monster.IconName)).GetString();
    return iconPath;
  }

  private static TrapInteractions GetTrapInteractions(JsonElement root)
  {
    var pitFallTrap = root.GetProperty(nameof(Monster.TrapInteractions.PitfallTrap)).GetBoolean();
    var shockTrap = root.GetProperty(nameof(Monster.TrapInteractions.ShockTrap)).GetBoolean();
    var vineTrap = root.GetProperty(nameof(Monster.TrapInteractions.VineTrap)).GetBoolean();
    var trapInteractions = new TrapInteractions(pitFallTrap, shockTrap, vineTrap);
    return trapInteractions;
  }

  private static List<Drop> GetDrops(JsonSerializerOptions options, JsonElement root)
  {
    var drops = new List<Drop>();
    var dropsArray = root.GetProperty(nameof(Monster.Drops)).EnumerateArray();
    while (dropsArray.MoveNext())
    {
      var dropJson = dropsArray.Current;

      var drop = JsonSerializer.Deserialize<Drop>(dropJson.GetRawText(), options);
      drops.Add(drop);
    }

    return drops;
  }

  private static List<FormWeakness> GetFormWeaknesses(JsonElement root)
  {
    var formWeaknessArray = root.GetProperty(nameof(Monster.FormWeaknesses)).EnumerateArray();
    var formWeaknesses = new List<FormWeakness>();
    foreach(var formWeaknessJson in formWeaknessArray)
    {
      var isAlt = formWeaknessJson.GetProperty("Form").GetString() == "alt";
      var description = formWeaknessJson.GetProperty("FormDescription").GetString();

      var weaknessesJson = formWeaknessJson.GetProperty("Weaknesses");
      var fireWeakness = weaknessesJson.GetProperty("Fire").GetInt32();
      var waterWeakness = weaknessesJson.GetProperty("Water").GetInt32();
      var thunderWeakness = weaknessesJson.GetProperty("Thunder").GetInt32();
      var iceWeakness = weaknessesJson.GetProperty("Ice").GetInt32();
      var dragonWeakness = weaknessesJson.GetProperty("Dragon").GetInt32();
      var poisonWeakness = weaknessesJson.GetProperty("Poison").GetInt32();
      var sleepWeakness = weaknessesJson.GetProperty("Sleep").GetInt32();
      var paralysisWeakness = weaknessesJson.GetProperty("Paralysis").GetInt32();
      var blastWeakness = weaknessesJson.GetProperty("Blast").GetInt32();
      var stunWeakness = weaknessesJson.GetProperty("Stun").GetInt32();

      var formWeakness = new FormWeakness(isAlt, description, fireWeakness, waterWeakness, thunderWeakness, iceWeakness, dragonWeakness, poisonWeakness, sleepWeakness, paralysisWeakness, blastWeakness, stunWeakness);
      formWeaknesses.Add(formWeakness);
    }

    return formWeaknesses;
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