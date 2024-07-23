using Serialization.Abstraction.Converters;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction;

public class JsonSerializor : ISerializor
{
  private JsonSerializerOptions _jsonSerializerOptions;

  public JsonSerializor(IEnumerable<IJsonConverter> jsonConverters)
  {
    _jsonSerializerOptions = new JsonSerializerOptions();

    foreach (var jsonConverter in jsonConverters)
    {
      if (jsonConverter is JsonConverter converter)
        _jsonSerializerOptions.Converters.Add(converter);
      else
        throw new InvalidOperationException("Cannot convert IJsonConverter to JsonConverter.");
    }
  }

  public string Serialize<T>(T obj)
  {
    return JsonSerializer.Serialize(obj);
  }

  public T Deserialize<T>(Assembly resourceAssembly, string resourceName, JsonSerializerOptions? options = null)
  {
    options ??= _jsonSerializerOptions;

    using var stream = resourceAssembly.GetManifestResourceStream(resourceName);
    using var reader = new StreamReader(stream);
    var json = reader.ReadToEnd();

    return JsonSerializer.Deserialize<T>(json, options);
  }
}