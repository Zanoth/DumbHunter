using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.insectGlaives;

namespace Serialization.Abstraction.Converters.Weapons;

public class InsectGlaiveConverter : JsonConverter<InsectGlaive>, IJsonConverter
{
  public override InsectGlaive? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();
    var sharpness = root.ReadSharpness();
    var kinsectMechanics = root.ReadKinsectMechanics();

    var insectGlaive = new InsectGlaive(baseInfo.id,
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
      kinsectMechanics);
    return insectGlaive;
  }

  public override void Write(Utf8JsonWriter writer, InsectGlaive insectGlaive, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(insectGlaive);
    writer.WriteSharpness(insectGlaive.Sharpness);
    writer.WriteKinsectMechanics(insectGlaive.KinsectMechanic);
  }
}