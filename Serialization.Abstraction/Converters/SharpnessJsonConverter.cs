using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class SharpnessJsonConverter : JsonConverter<Sharpness>, IJsonConverter
{
  public override Sharpness Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var isMaxed = root.GetProperty(nameof(Sharpness.IsMaxed)).GetBoolean();
    var sharpnessSections = new List<SharpnessSection>();

    var sharpnessJsonArray = root.GetProperty(nameof(Sharpness.Sections)).EnumerateArray();
    while (sharpnessJsonArray.MoveNext())
    {
      var sharpnessJson = sharpnessJsonArray.Current;

      var colorStr = sharpnessJson.GetProperty(nameof(SharpnessSection.Color)).GetString();
      var sharpnessValue = sharpnessJson.GetProperty(nameof(SharpnessSection.Value)).GetInt32();

      var sharpnessSection = new SharpnessSection(Color.FromName(colorStr), sharpnessValue);
      sharpnessSections.Add(sharpnessSection);
    }
   
    var sharpness = new Sharpness(isMaxed, sharpnessSections);
    return sharpness;
  }

  public override void Write(Utf8JsonWriter writer, Sharpness sharpnessObj, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WriteBoolean(nameof(Sharpness.IsMaxed), sharpnessObj.IsMaxed);

    writer.WritePropertyName(nameof(Sharpness.Sections));
    writer.WriteStartArray();
    foreach (var section in sharpnessObj.Sections)
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