using MhW.Wpf.Views;
using MHW.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using MhW.Wpf;

namespace MHW.Wpf;

public partial class App : PrismApplication
{
  public App()
  {
    Console.WriteLine("Monster Hunter World, Wpf, has launched - initializing now");
  }

  public static IServiceCollection ServiceCollection { get; set; } = new ServiceCollection();

  protected override Window CreateShell()
  {
    Console.WriteLine("Creating UI Shell");

    var mainWindow = Container.Resolve<MainWindow>();
    mainWindow.DataContext = Container.Resolve<MainWindowViewModel>();
    return mainWindow;
  }

  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    containerRegistry.RegisterSingleton<MainWindowViewModel>();
    containerRegistry.RegisterSingleton<CraftableGearViewModel>();
    containerRegistry.RegisterSingleton<EquippedGearViewModel>();
    containerRegistry.RegisterSingleton<GearManagementViewModel>();
    containerRegistry.RegisterSingleton<MonsterViewModel>();
    containerRegistry.RegisterSingleton<MainWindowViewModel>();

    containerRegistry.RegisterForNavigation<GearManagementView, GearManagementViewModel>();
    containerRegistry.RegisterForNavigation<MonsterView, MonsterViewModel>();

    var regionManager = Container.Resolve<IRegionManager>();
    regionManager.RegisterViewWithRegion("EquippedGearRegion", typeof(EquippedGearViewModel));
    regionManager.RegisterViewWithRegion("CraftableGearRegion", typeof(CraftableGearViewModel));
  }

  protected override IContainerExtension CreateContainerExtension() => new MicrosoftDIContainerExtension(ServiceCollection);
}