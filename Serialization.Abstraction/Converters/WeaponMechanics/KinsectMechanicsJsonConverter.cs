using System.Text.Json;
using System.Text.Json.Serialization;
using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

namespace Serialization.Abstraction.Converters.WeaponMechanics;

public class KinsectMechanicsJsonConverter : JsonConverter<KinsectMechanics>, IJsonConverter
{
  public override KinsectMechanics Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var kinsectBonusStr = root.GetProperty(nameof(KinsectMechanics.KinsectBonusType)).GetString();
    var bonusType = (KinsectBonusType)Enum.Parse(typeof(KinsectBonusType), kinsectBonusStr);

    var kinsectMechanics = new KinsectMechanics(bonusType);
    return kinsectMechanics;
  }

  public override void Write(Utf8JsonWriter writer, KinsectMechanics kinsectMechanics, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(KinsectMechanics.KinsectBonusType), kinsectMechanics.KinsectBonusType.ToString());
    writer.WriteEndObject();
  }
}