using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.Hammers;

namespace Serialization.Abstraction.Converters.Weapons;

public class HammerConverter : JsonConverter<Hammer>, IJsonConverter
{
  public override Hammer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();

    var sharpness = root.ReadSharpness();

    var hammer = new Hammer(baseInfo.id,
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
    return hammer;
  }

  public override void Write(Utf8JsonWriter writer, Hammer hammer, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(hammer);
    writer.WriteSharpness(hammer.Sharpness);
  }
}