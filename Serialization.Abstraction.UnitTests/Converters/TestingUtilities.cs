using System.Text;

namespace Serialization.Abstraction.UnitTests.Converters;

internal static class TestingUtilities
{
  internal static string MinifyJson(string json)
  {
    using var document = JsonDocument.Parse(json);
    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);
    document.WriteTo(writer);
    writer.Flush();
    return Encoding.UTF8.GetString(stream.ToArray());
  }
}