using Microsoft.Extensions.DependencyInjection;


namespace MhW.Wpf;

//Hack: Implementation is VERY naive, could lead to issues
public class MicrosoftDIContainerExtension : IContainerExtension<IServiceProvider>
{
  private readonly IServiceCollection _serviceCollection;
  private IServiceProvider _serviceProvider;

  public MicrosoftDIContainerExtension(IServiceCollection serviceCollection)
  {
    _serviceCollection = serviceCollection;
    _serviceProvider = _serviceCollection.BuildServiceProvider();
  }

  public void FinalizeExtension() => _serviceProvider = _serviceCollection.BuildServiceProvider();

  public IServiceProvider Instance => _serviceProvider;

  // Implement other IContainerExtension and IContainerProvider methods by delegating to _serviceProvider...
  public object Resolve(Type type) => _serviceProvider.GetService(type);

  public object Resolve(Type type, params (Type Type, object Instance)[] parameters) => Resolve(type);

  public object Resolve(Type type, string name) => Resolve(type);

  public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters) => Resolve(type);

  public IScopedProvider CreateScope()
  {
    var serviceScopeFactor = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
    var scope = serviceScopeFactor.CreateScope();
    return new ScopedProvider(scope.ServiceProvider);
  }

  public IScopedProvider CurrentScope { get; }
  public IContainerRegistry RegisterInstance(Type type, object instance)
  {
    _serviceCollection.AddTransient(type, _ => instance);
    return this;
  }

  public IContainerRegistry RegisterInstance(Type type, object instance, string name) => RegisterInstance(type, instance);

  public IContainerRegistry RegisterSingleton(Type from, Type to)
  {
    _serviceCollection.AddSingleton(from, to);
    return this;
  }

  public IContainerRegistry RegisterSingleton(Type from, Type to, string name) => RegisterSingleton(from, to);

  public IContainerRegistry RegisterSingleton(Type type, Func<object> factoryMethod) => RegisterSingleton(type, _ => factoryMethod());

  public IContainerRegistry RegisterSingleton(Type type, Func<IContainerProvider, object> factoryMethod) => RegisterSingleton(type, _ => factoryMethod(this));

  public IContainerRegistry RegisterManySingleton(Type type, params Type[] serviceTypes)
  {
    foreach (var serviceType in serviceTypes)
      _serviceCollection.AddSingleton(serviceType, type);
    return this;
  }

  public IContainerRegistry Register(Type from, Type to)
  {
    _serviceCollection.AddTransient(from, to);
    return this;
  }

  public IContainerRegistry Register(Type from, Type to, string name) => Register(from, to);

  public IContainerRegistry Register(Type type, Func<object> factoryMethod) => Register(type, _ => factoryMethod());

  public IContainerRegistry Register(Type type, Func<IContainerProvider, object> factoryMethod) => Register(type, _ => factoryMethod(this));

  public IContainerRegistry RegisterMany(Type type, params Type[] serviceTypes)
  {
    foreach (var serviceType in serviceTypes)
      _serviceCollection.AddTransient(serviceType, type);
    return this;
  }

  public IContainerRegistry RegisterScoped(Type from, Type to)
  {
    _serviceCollection.AddScoped(from, to);
    return this;
  }

  public IContainerRegistry RegisterScoped(Type type, Func<object> factoryMethod) => RegisterScoped(type, _ => factoryMethod());

  public IContainerRegistry RegisterScoped(Type type, Func<IContainerProvider, object> factoryMethod) => RegisterScoped(type, _ => factoryMethod(this));

  public bool IsRegistered(Type type)
  {
    try
    {
      return _serviceProvider.GetService(type) != null;
    }
    catch
    {
      return false;
    }
  }

  public bool IsRegistered(Type type, string name) => IsRegistered(type);
}