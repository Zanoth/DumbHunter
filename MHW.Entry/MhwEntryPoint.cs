using MHW.Console;
using Microsoft.Extensions.DependencyInjection;
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
    var uiType = "Console";
    //var uiType = "Wpf";

    Console.WriteLine($"UI type chosen: {uiType}");

    var serviceCollection = new ServiceCollection();


    var typeFinder = new ReflectionTypeFinder();

    var commonServices = new List<IServiceRegistrar>
    {
      new MhwDataContext(),
      new SerilizationContext(typeFinder)
    };

    RegisterServices(serviceCollection, commonServices);

    if (uiType == "Wpf")
      LaunchWpf(args, serviceCollection);
    else if (uiType == "Console")
      LaunchConsole(args, serviceCollection);
  }

  private static void LaunchConsole(string[] args, IServiceCollection serviceCollection)
  {
    RegisterServices(serviceCollection, new List<IServiceRegistrar>
    {
      new MhwConsoleContext(),
    });

    ConsoleProgram.ServiceCollection = serviceCollection;
    ConsoleProgram.Main(args);
  }

  private static void LaunchWpf(string[] args, IServiceCollection serviceCollection)
  {
    throw new NotImplementedException();
    //RegisterServices(serviceCollection, new List<IServiceRegistrar>
    //{
    //  new MhwWpfContext(),
    //});

    //Wpf.App.ServiceCollection = serviceCollection;
    //var worldWpfApp = new Wpf.App(); 
    //worldWpfApp.Run();
  }


  private static void RegisterServices(IServiceCollection services, List<IServiceRegistrar> assemblyServiceRegistrars)
  {
    foreach (var registrar in assemblyServiceRegistrars)
    {
      Console.WriteLine($"Registering service: {registrar}");
      registrar.RegisterServices(services);
    }
  }
}