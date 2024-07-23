using Microsoft.Extensions.DependencyInjection;

namespace MhW.Wpf
{
  //Hack: Implementation is VERY naive, could lead to issues
  public class ScopedProvider : IScopedProvider, IDisposable
  {
    private IServiceScope _scope;
    private IServiceProvider _serviceProvider;
    private bool _disposed = false;

    public ScopedProvider(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      _scope = (_serviceProvider as IServiceScopeFactory)?.CreateScope();
    }

    public object Resolve(Type type) => _scope.ServiceProvider.GetService(type);

    public object Resolve(Type type, params (Type Type, object Instance)[] parameters) => Resolve(type);

    public object Resolve(Type type, string name) => Resolve(type);

    public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters) => Resolve(type);

    public IScopedProvider CreateScope()
    {
      return new ScopedProvider(_serviceProvider);
    }

    public IScopedProvider CurrentScope => this;

    //TODO: Fix Compiler warning?
    public void Dispose()
    {
      if (_disposed) return;
      _scope?.Dispose();
      _disposed = true;
    }

    public IScopedProvider CreateChildScope()
    {
      return CreateScope(); // In this simple implementation, it's the same as creating a new scope.
    }

    public bool IsAttached { get; set; }
  }
}