using System.Text.Json;
using System.Text.Json.Serialization;
using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

namespace Serialization.Abstraction.Converters.WeaponMechanics;

public class ShellingMechanicsJsonConverter : JsonConverter<ShellingMechanics>, IJsonConverter
{
  public override ShellingMechanics Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var shellingTypeStr = root.GetProperty(nameof(ShellingMechanics.ShellingType)).GetString();
    var shellingType = (ShellingType)Enum.Parse(typeof(ShellingType), shellingTypeStr);

    var shellingLevel = root.GetProperty(nameof(ShellingMechanics.ShellingLevel)).GetInt32();

    var shellingMechanics = new ShellingMechanics(shellingType, shellingLevel);
    return shellingMechanics;
  }

  public override void Write(Utf8JsonWriter writer, ShellingMechanics shellingMechanics, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(ShellingMechanics.ShellingType), shellingMechanics.ShellingType.ToString());
    writer.WriteNumber(nameof(ShellingMechanics.ShellingLevel), shellingMechanics.ShellingLevel);
    writer.WriteEndObject();
  }
}