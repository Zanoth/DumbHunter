using System.Reflection;
using System.Text.Json;

namespace Serialization.Abstraction;

public interface ISerializor
{
  string Serialize<T>(T obj);
  T Deserialize<T>(Assembly resourceAssembly, string resourceName, JsonSerializerOptions? options = null);
}