using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.Bows;
using SharedDataModels.Abstractions.Gear.Weapons.ChargeBlades;

namespace Serialization.Abstraction.Converters.Weapons;

public class BowJsonConverter : JsonConverter<Bow>, IJsonConverter
{
  public override Bow? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();

    var coatingMechanics = root.ReadCoatings();

    var bow = new Bow(baseInfo.id,
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
      coatingMechanics);
    return bow;
  }

  public override void Write(Utf8JsonWriter writer, Bow bow, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(bow);

    writer.WriteCoatings(bow.CoatingMechanic);
  }
}