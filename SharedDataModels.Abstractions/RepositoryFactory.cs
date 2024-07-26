using Microsoft.Extensions.DependencyInjection;

namespace SharedDataModels.Abstractions;

public class RepositoryFactory<TID, T> : IRepositoryFactory<TID, T>
{
  private readonly IServiceProvider _serviceProvider;

  public RepositoryFactory(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public IRepository<TID, T> CreateRepository()
  {
    var service = _serviceProvider.GetRequiredService<IRepository<TID, T>>();
    return service;
  }
}