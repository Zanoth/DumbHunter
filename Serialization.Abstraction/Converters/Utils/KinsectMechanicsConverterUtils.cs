using System.Text.Json;
using SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Kinsects;
using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace Serialization.Abstraction.Converters.Utils;

public static class KinsectMechanicsConverterUtils
{
  public static KinsectMechanic ReadKinsectMechanics(this JsonElement root)
  {
    var kinsectBonusStr = root.GetProperty(nameof(KinsectMechanic.KinsectBonus)).GetString();
    var bonusType = Enum.Parse<KinsectBonus>(kinsectBonusStr!);

    var kinsectMechanics = new KinsectMechanic(bonusType);
    return kinsectMechanics;
  }
  public static void WriteKinsectMechanics(this Utf8JsonWriter writer, KinsectMechanic kinsectMechanic)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(KinsectMechanic.KinsectBonus), kinsectMechanic.KinsectBonus.ToString());
    writer.WriteEndObject();
  }
}