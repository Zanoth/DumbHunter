using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.HuntingHorns;

namespace Serialization.Abstraction.Converters.Weapons;

public class HuntingHornConverter : JsonConverter<HuntingHorn>, IJsonConverter
{
  public override HuntingHorn? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();
    var sharpness = root.ReadSharpness();
    var melodyMechanic = root.ReadMelodies();

    var huntingHorn = new HuntingHorn(baseInfo.id,
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
      melodyMechanic);
    return huntingHorn;
  }

  public override void Write(Utf8JsonWriter writer, HuntingHorn huntingHorn, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(huntingHorn);
    writer.WriteSharpness(huntingHorn.Sharpness);
    writer.WriteMelodies(huntingHorn.MelodyMechanic);
  }
}