using MHW.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using SharedDataModels.Abstractions;

namespace MHW.Wpf;

//Refactor: I'm using two different IOC technologies at the moment - I should probably see if I can use Prism with Microsoft's IOC
public class MhwWpfContext : ContextBase, IServiceRegistrar
{
  public void RegisterServices(IServiceCollection services)
  {
    return; 
  }
}