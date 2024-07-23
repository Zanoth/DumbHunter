using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class DefenseStatsJsonConverter : JsonConverter<DefenseStats>, IJsonConverter
{
  public override DefenseStats Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var baseDef = -1;
    var maxDef = -1;
    var argumentedMaxDef = -1;
    var fireDef = -1;
    var waterDef = -1;
    var thunderDef = -1;
    var iceDef = -1;
    var dragonDef = -1;

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.PropertyName)
      {
        var propertyName = reader.GetString();
        reader.Read(); // Move to the value
        switch (propertyName)
        {
          case nameof(DefenseStats.BaseDef):
            baseDef = reader.GetInt32();
            break;
          case nameof(DefenseStats.MaxDef):
            maxDef = reader.GetInt32();
            break;
          case nameof(DefenseStats.ArgumentedMaxDef):
            argumentedMaxDef = reader.GetInt32();
            break;
          case nameof(DefenseStats.FireDef):
            fireDef = reader.GetInt32();
            break;
          case nameof(DefenseStats.WaterDef):
            waterDef = reader.GetInt32();
            break;
          case nameof(DefenseStats.ThunderDef):
            thunderDef = reader.GetInt32();
            break;
          case nameof(DefenseStats.IceDef):
            iceDef = reader.GetInt32();
            break;
          case nameof(DefenseStats.DragonDef):
            dragonDef = reader.GetInt32();
            break;
        }
      }
    }

    var defenseStats = new DefenseStats(baseDef, maxDef, argumentedMaxDef, fireDef, waterDef, thunderDef, iceDef, dragonDef);
    return defenseStats;
  }

  public override void Write(Utf8JsonWriter writer, DefenseStats value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteNumber(nameof(DefenseStats.BaseDef), value.BaseDef);
    writer.WriteNumber(nameof(DefenseStats.MaxDef), value.MaxDef);
    writer.WriteNumber(nameof(DefenseStats.ArgumentedMaxDef), value.ArgumentedMaxDef);
    writer.WriteNumber(nameof(DefenseStats.FireDef), value.FireDef);
    writer.WriteNumber(nameof(DefenseStats.WaterDef), value.WaterDef);
    writer.WriteNumber(nameof(DefenseStats.ThunderDef), value.ThunderDef);
    writer.WriteNumber(nameof(DefenseStats.IceDef), value.IceDef);
    writer.WriteNumber(nameof(DefenseStats.DragonDef), value.DragonDef);

    writer.WriteEndObject();
  }
}