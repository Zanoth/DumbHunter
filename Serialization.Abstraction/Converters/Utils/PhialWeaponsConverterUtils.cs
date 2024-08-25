using System.Text.Json;
using SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Phials;
using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace Serialization.Abstraction.Converters.Utils;

public static class PhialWeaponsConverterUtils
{
  public static PhialMechanic ReadPhialMechanics(this JsonElement root)
  {
    var phialJson = root.GetProperty(nameof(PhialMechanic));

    var phialTypeStr = phialJson.GetProperty(nameof(PhialMechanic.PhialType)).GetString();
    var phialType = (PhialType)Enum.Parse(typeof(PhialType), phialTypeStr);

    var phialPower = phialJson.GetProperty(nameof(PhialMechanic.PhialPower)).GetInt32();

    var phialMechanics = new PhialMechanic(phialType, phialPower);
    return phialMechanics;
  }
  public static void WritePhialMechanics(this Utf8JsonWriter writer, PhialMechanic phialMechanic)
  {
    writer.WriteStartObject();

    writer.WriteString(nameof(PhialMechanic.PhialType), phialMechanic.PhialType.ToString());
    writer.WriteNumber(nameof(PhialMechanic.PhialPower), phialMechanic.PhialPower);

    writer.WriteEndObject();
  }
}