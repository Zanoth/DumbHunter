using SharedDataModels.Abstractions.Gear.Weapons.Stats;
using System.Drawing;
using System.Text.Json;

namespace Serialization.Abstraction.Converters.Utils;

public static class SharpnessWeaponConverterUtils
{
  public static Sharpness ReadSharpness(this JsonElement root)
  {
    var sharpnessJson = root.GetProperty(nameof(Sharpness));

    var isMaxed = sharpnessJson.GetProperty(nameof(Sharpness.IsMaxed)).GetBoolean();
    var sharpnessSections = new List<SharpnessSection>();

    var sharpnessJsonArray = sharpnessJson.GetProperty(nameof(Sharpness.Sections)).EnumerateArray();
    while (sharpnessJsonArray.MoveNext())
    {
      var sectionJson = sharpnessJsonArray.Current;

      var colorStr = sectionJson.GetProperty(nameof(SharpnessSection.Color)).GetString();
      var sharpnessValue = sectionJson.GetProperty(nameof(SharpnessSection.Value)).GetInt32();

      var sharpnessSection = new SharpnessSection(Color.FromName(colorStr), sharpnessValue);
      sharpnessSections.Add(sharpnessSection);
    }

    var sharpness = new Sharpness(isMaxed, sharpnessSections);
    return sharpness;
  }
  public static void WriteSharpness(this Utf8JsonWriter writer, Sharpness sharpness)
  {
    writer.WriteStartObject();

    writer.WriteBoolean(nameof(Sharpness.IsMaxed), sharpness.IsMaxed);

    writer.WritePropertyName(nameof(Sharpness.Sections));
    writer.WriteStartArray();
    foreach (var section in sharpness.Sections)
    {
      writer.WriteStartObject();
      writer.WriteString(nameof(SharpnessSection.Color), section.Color.Name);
      writer.WriteNumber(nameof(SharpnessSection.Value), section.Value);
      writer.WriteEndObject();
    }
    writer.WriteEndArray();

    writer.WriteEndObject();
  }

}