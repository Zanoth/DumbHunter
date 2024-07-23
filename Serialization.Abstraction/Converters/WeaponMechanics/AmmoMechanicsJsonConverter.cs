using System.Text.Json;
using System.Text.Json.Serialization;
using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

namespace Serialization.Abstraction.Converters.WeaponMechanics;

public class AmmoMechanicsJsonConverter : JsonConverter<AmmoMechanics>, IJsonConverter
{
  public override AmmoMechanics Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    throw new NotImplementedException();

    //using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    //var root = doc.RootElement;

    //var phialTypeStr = root.GetProperty(nameof(PhialMechanics.PhialType)).GetString();
    //var phialType = (PhialType)Enum.Parse(typeof(PhialType), phialTypeStr);

    //var phialPower = root.GetProperty(nameof(PhialMechanics.PhialPower)).GetInt32();

    //var gunLanceMechanics = new AmmoMechanics(phialType, phialPower);
    //return gunLanceMechanics;
  }

  public override void Write(Utf8JsonWriter writer, AmmoMechanics value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();

    //writer.WriteStartObject();

    //writer.WriteString(nameof(PhialMechanics.PhialType), value.PhialType.ToString());
    //writer.WriteNumber(nameof(PhialMechanics.PhialPower), value.PhialPower);
   
    //writer.WriteEndObject();
  }
}