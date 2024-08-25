using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.Lances;

namespace Serialization.Abstraction.Converters.Weapons;

public class LanceConverter : JsonConverter<Lance>, IJsonConverter
{
  public override Lance? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();
    var sharpness = root.ReadSharpness();

    var lance = new Lance(baseInfo.id,
      baseInfo.name,
      baseInfo.rarity,
      baseInfo.attack,
      baseInfo.affinity,
      baseInfo.defense,
      baseInfo.elementHidden,
      baseInfo.elementalStats,
      baseInfo.elderseal,
      baseInfo.decorationSlots,
      baseInfo.skills,
      sharpness);
    return lance;
  }

  public override void Write(Utf8JsonWriter writer, Lance lance, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(lance);
    writer.WriteSharpness(lance.Sharpness);
  }
}