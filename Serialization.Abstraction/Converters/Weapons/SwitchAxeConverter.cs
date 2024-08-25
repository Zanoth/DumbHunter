using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.SwitchAxes;

namespace Serialization.Abstraction.Converters.Weapons;

public class SwitchAxeConverter : JsonConverter<SwitchAxe>, IJsonConverter
{
  public override SwitchAxe? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();
    var sharpness = root.ReadSharpness();
    var phials = root.ReadPhialMechanics();

    var switchAxe = new SwitchAxe(baseInfo.id,
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
      sharpness,
      phials);
    return switchAxe;
  }

  public override void Write(Utf8JsonWriter writer, SwitchAxe switchAxe, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(switchAxe);
    writer.WriteSharpness(switchAxe.Sharpness);
    writer.WritePhialMechanics(switchAxe.PhialMechanic);
  }
}