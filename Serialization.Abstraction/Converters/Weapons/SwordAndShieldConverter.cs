using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.SwordAndShields;

namespace Serialization.Abstraction.Converters.Weapons;

public class SwordAndShieldConverter : JsonConverter<SwordAndShield>, IJsonConverter
{
  public override SwordAndShield? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();
    var sharpness = root.ReadSharpness();

    var swordAndShield = new SwordAndShield(baseInfo.id,
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
    return swordAndShield;
  }

  public override void Write(Utf8JsonWriter writer, SwordAndShield swordAndShield, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(swordAndShield);
    writer.WriteSharpness(swordAndShield.Sharpness);
  }
}