using Prism.Ioc;

namespace SharedDataModels.Abstractions;

public interface IServiceRegistrator
{
  void RegisterServices(IContainerRegistry container);
}