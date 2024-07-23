using Microsoft.Extensions.DependencyInjection;
using Serialization.Abstraction.Converters;

namespace Serialization.Abstraction;

public class SerilizationContext : ContextBase, IServiceRegistrar
{
  private readonly ITypeFinder _typeFinder;

  public SerilizationContext(ITypeFinder typeFinder)
  {
    _typeFinder = typeFinder;
  }

  public void RegisterServices(IServiceCollection services)
  {
    services.AddScoped<ISerializor, JsonSerializor>();

    AutoRegisterServices(services, typeof(IJsonConverter));
  }

  private void AutoRegisterServices(IServiceCollection services, Type serviceType)
  {
    var implementingTypes = _typeFinder.GetImplementingTypes(serviceType);

    foreach (var implementingType in implementingTypes)
      RegisterService(services, serviceType, implementingType);
  }
}