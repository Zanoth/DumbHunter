using Microsoft.Extensions.DependencyInjection;

namespace SharedDataModels.Abstractions;

public interface IRepository<TID, T>
{
  public T Get(TID id);
  public Task<T> GetAsync(TID id);
  public IEnumerable<T> GetAll();
  public Task<IEnumerable<T>> GetAllAsync();
}

public interface IRepositoryFactory<TID, T>
{
  IRepository<TID, T> CreateRepository();
}

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