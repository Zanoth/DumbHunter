using System.Reflection;

namespace Serialization.Abstraction;

public class ReflectionTypeFinder : ITypeFinder
{
  public IEnumerable<Type> GetImplementingTypes(Type serviceType)
  {
    var assembly = Assembly.GetExecutingAssembly();
    return assembly.GetTypes()
      .Where(type => serviceType.IsAssignableFrom(type) 
                     && type is { IsInterface: false, IsAbstract: false });
  }
}