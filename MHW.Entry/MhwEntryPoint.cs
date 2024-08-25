using MHW.Console;
using MHW.Wpf;
using Prism.DryIoc;
using Prism.Ioc;
using Serialization.Abstraction;

namespace MHW.Entry;

using Console = System.Console;

public static class MhwEntryPoint
{
  public static void Main(string[] args)
  {
    Console.WriteLine("Welcome - Launching Support program for Monster Hunter World");
    Console.WriteLine("------------------------------------------------------------");

    // Assuming we have retrieved the UI type from args
    //var uiType = "Console";
    var uiType = "Wpf";

    Console.WriteLine($"UI type chosen: {uiType}");

    var container = new DryIocContainerExtension();


    var typeFinder = new ReflectionTypeFinder();

    var commonServices = new List<IServiceRegistrator>
    {
      new MhwDataContext(),
      new SerilizationContext(typeFinder)
    };

    RegisterServices(container, commonServices);

    if (uiType == "Wpf")
      LaunchWpf(args, container);
    else if (uiType == "Console")
      LaunchConsole(args, container);
  }

  private static void LaunchConsole(string[] args, IContainerExtension container)
  {
    RegisterServices(container, new List<IServiceRegistrator>
    {
      new MhwConsoleContext(),
    });

    ConsoleProgram.Container = container;
    ConsoleProgram.Main(args);
  }

  private static void LaunchWpf(string[] args, IContainerExtension container)
  {
    //RegisterServices(container, new List<IServiceRegistrator>
    //{
    //  new MhwWpfContext(),
    //});

    App.SetContainer(container);
    var worldWpfApp = new App();
    worldWpfApp.Run();
  }


  private static void RegisterServices(IContainerRegistry services, List<IServiceRegistrator> assemblyServiceRegistrars)
  {
    foreach (var registrar in assemblyServiceRegistrars)
    {
      Console.WriteLine($"Registering service: {registrar}");
      registrar.RegisterServices(services);
    }
  }
}