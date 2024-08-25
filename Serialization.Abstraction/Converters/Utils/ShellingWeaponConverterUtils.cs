using System.Text.Json;
using SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Shellings;
using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace Serialization.Abstraction.Converters.Utils;

public static class ShellingWeaponConverterUtils
{
  public static ShellingMechanic ReadShelling(this JsonElement root)
  {
    var shellingJson = root.GetProperty(nameof(ShellingMechanic));

    var shellingTypeStr = shellingJson.GetProperty(nameof(ShellingMechanic.ShellingType)).GetString();
    var shellingType = (ShellingType)Enum.Parse(typeof(ShellingType), shellingTypeStr);

    var shellingLevel = shellingJson.GetProperty(nameof(ShellingMechanic.ShellingLevel)).GetInt32();

    var shellingMechanics = new ShellingMechanic(shellingType, shellingLevel);
    return shellingMechanics;
  }
  public static void WriteShelling(this Utf8JsonWriter writer, ShellingMechanic shellingMechanic)
  {
    writer.WriteStartObject();

    writer.WriteString(nameof(ShellingMechanic.ShellingType), shellingMechanic.ShellingType.ToString());
    writer.WriteNumber(nameof(ShellingMechanic.ShellingLevel), shellingMechanic.ShellingLevel);

    writer.WriteEndObject();
  }
}