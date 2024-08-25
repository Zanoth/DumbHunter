using MHW.Wpf.ViewModels;
using MHW.Wpf.Views;
using Prism;
using Prism.Ioc;
using System.Collections.ObjectModel;
using System.Windows;
using Prism.DryIoc;

namespace MHW.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplicationBase
{
  public static IContainerExtension? Container { get; private set; }

  public static void SetContainer(IContainerExtension container) => Container = container;

  protected override void OnStartup(StartupEventArgs e)
  {
    CreateContainerExtension();

    base.OnStartup(e);

    SetupDesignResources();

    var mainWindow = CreateShell();
    mainWindow.Show();
  }


  protected override IContainerExtension CreateContainerExtension()
  {
    if (Container == null) 
      Container = new DryIocContainerExtension();

    return Container;
  }

  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    var context = new MhwWpfContext();
    context.RegisterServices(containerRegistry);
  }

  protected override Window CreateShell()
  {
    return Container.Resolve<MainWindow>();
  }



  private void SetupDesignResources()
  {
    Resources.MergedDictionaries.AddRange(new ResourceDictionary[]
    {
      new()
      {
        Source = new Uri("pack://application:,,,/Wpf.Design;component/Styles/BaseStyle.xaml")
      }
    });
  }
}