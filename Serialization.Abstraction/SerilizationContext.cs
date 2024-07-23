using Microsoft.Extensions.DependencyInjection;
using Serialization.Abstraction.Converters;
using System.Reflection;

namespace Serialization.Abstraction;

public class SerilizationContext : ContextBase, IServiceRegistrar
{
  public void RegisterServices(IServiceCollection services)
  {
    services.AddScoped<ISerializor, JsonSerializor>();

    AutoRegisterServices(services, typeof(IJsonConverter));
  }

  private void AutoRegisterServices(IServiceCollection services, Type serviceType)
  {
    var assembly = Assembly.GetExecutingAssembly();
    var implementingTypes = assembly.GetTypes()
      .Where(type => serviceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

    foreach (var implementingType in implementingTypes)
      RegisterService(services, serviceType, implementingType);
  }
}