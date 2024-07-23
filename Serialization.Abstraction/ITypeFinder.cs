namespace Serialization.Abstraction;

public interface ITypeFinder
{
  IEnumerable<Type> GetImplementingTypes(Type serviceType);
}