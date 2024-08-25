using MHW.Wpf.Providers;
using MHW.Wpf.ViewModels;
using MHW.Wpf.Views;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Prism.Regions;
using SharedDataModels.Abstractions;

namespace MHW.Wpf;

public class MhwWpfContext : IServiceRegistrator
{
  public void RegisterServices(IContainerRegistry container)
  {
    container.RegisterSingleton<IRegionManager, RegionManager>();

    container.RegisterSingleton<MainWindow>();
    container.RegisterSingleton<MainWindowViewModel>();
    container.RegisterForNavigation<GearManagementViewModel>();
    container.RegisterForNavigation<MonsterListViewModel>();

    var configuration = GetConfiguration();
    container.RegisterInstance<IConfiguration>(configuration);
    container.RegisterSingleton<IIconDirectoryPathProvider, IconDirectoryPathProvider>();
  }


  private IConfigurationRoot GetConfiguration()
  {
    var builder = new ConfigurationBuilder()
      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    return builder.Build();
  }
}
