using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters.WeaponMechanics;

public class PhialMechanicsJsonConverter : JsonConverter<PhialMechanics>, IJsonConverter
{
  public override PhialMechanics Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var phialTypeStr = root.GetProperty(nameof(PhialMechanics.PhialType)).GetString();
    var phialType = (PhialType)Enum.Parse(typeof(PhialType), phialTypeStr);

    var phialPower = root.GetProperty(nameof(PhialMechanics.PhialPower)).GetInt32();

    var phialMechanics = new PhialMechanics(phialType, phialPower);
    return phialMechanics;
  }

  public override void Write(Utf8JsonWriter writer, PhialMechanics value, JsonSerializerOptions options)
  {

    writer.WriteStartObject();

    writer.WriteString(nameof(PhialMechanics.PhialType), value.PhialType.ToString());
    writer.WriteNumber(nameof(PhialMechanics.PhialPower), value.PhialPower);
   
    writer.WriteEndObject();
  }
}