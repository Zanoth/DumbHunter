using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.DualBlades;

namespace Serialization.Abstraction.Converters.Weapons;

public class DualBladesConverter : JsonConverter<DualBlades>, IJsonConverter
{
  public override DualBlades? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();

    var sharpness = root.ReadSharpness();

    var dualBlades = new DualBlades(baseInfo.id,
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
    return dualBlades;
  }

  public override void Write(Utf8JsonWriter writer, DualBlades dualBlades, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(dualBlades);
    writer.WriteSharpness(dualBlades.Sharpness);
  }
}