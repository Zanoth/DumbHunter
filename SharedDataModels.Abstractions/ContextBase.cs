using Microsoft.Extensions.DependencyInjection;

namespace SharedDataModels.Abstractions;

public abstract class ContextBase
{
  public void RegisterService(IServiceCollection services, Type serviceInterface, Type serviceImplementation)
  {
    Console.WriteLine($"Registering service: {serviceImplementation}");
    services.AddScoped(serviceInterface, serviceImplementation);
  }
}