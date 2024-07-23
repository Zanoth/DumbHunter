using Microsoft.Extensions.DependencyInjection;

namespace SharedDataModels.Abstractions;

public interface IServiceRegistrar
{
  void RegisterServices(IServiceCollection services);
}