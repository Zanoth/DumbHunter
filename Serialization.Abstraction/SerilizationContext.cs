using Prism.Ioc;
using Serialization.Abstraction.Converters;

namespace Serialization.Abstraction;

public class SerilizationContext : IServiceRegistrator
{
  private readonly ITypeFinder _typeFinder;

  public SerilizationContext(ITypeFinder typeFinder)
  {
    _typeFinder = typeFinder;
  }

  public void RegisterServices(IContainerRegistry container)
  {
    container.RegisterSingleton<ISerializor, JsonSerializor>();

    AutoRegisterServices(container, typeof(IJsonConverter));
  }

  private void AutoRegisterServices(IContainerRegistry container, Type serviceInterfaceType)
  {
    var implementingTypes = _typeFinder.GetImplementingTypes(serviceInterfaceType);

    foreach (var implementingType in implementingTypes)
      container.RegisterSingleton(serviceInterfaceType, implementingType);
  }
}