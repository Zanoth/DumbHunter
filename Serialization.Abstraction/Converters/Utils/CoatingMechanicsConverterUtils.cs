using System.Text.Json;
using SharedDataModels.Abstractions.Gear.Weapons;
using SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Coatings;

namespace Serialization.Abstraction.Converters.Utils;

public static class CoatingMechanicsConverterUtils
{
  public static CoatingMechanic ReadCoatings(this JsonElement root)
  {
    var coatingArrayJson = root.GetProperty(nameof(CoatingMechanic.Coatings)).EnumerateArray();

    var coatings = new List<Coating>();
    foreach (var coatingJson in coatingArrayJson)
    {
      var coatingTypeStr = coatingJson.GetProperty(nameof(Coating.Type)).GetString();
      var coatingType = Enum.Parse<CoatingType>(coatingTypeStr!);

      var coatingEnabled = coatingJson.GetProperty(nameof(Coating.Enabled)).GetBoolean();
      var coating = new Coating(coatingType, coatingEnabled);
      coatings.Add(coating);
    }

    var coatingMechanics = new CoatingMechanic(coatings);
    return coatingMechanics;
  }
  public static void WriteCoatings(this Utf8JsonWriter writer, CoatingMechanic coatingMechanic)
  {
    writer.WriteStartObject();

    foreach (var coating in coatingMechanic.Coatings)
      writer.WriteBoolean(coating.Type.ToString(), coating.Enabled);

    writer.WriteEndObject();
  }
}