using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.ChargeBlades;

namespace Serialization.Abstraction.Converters.Weapons;

public class ChargeBladeConverter : JsonConverter<ChargeBlade>, IJsonConverter
{
  public override ChargeBlade? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();

    var sharpness = root.ReadSharpness();
    var phialMechanics = root.ReadPhialMechanics();

    var chargeBlade = new ChargeBlade(baseInfo.id,
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
      phialMechanics);
    return chargeBlade;
  }

  public override void Write(Utf8JsonWriter writer, ChargeBlade chargeBlade, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(chargeBlade);

    writer.WriteSharpness(chargeBlade.Sharpness);
    writer.WritePhialMechanics(chargeBlade.PhialMechanic);
  }
}