using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Serialization.Abstraction.UnitTests;

public class SerilizationContextFixture
{
  [Fact]
  public void RegisterServices_GivenServiceCollection_ShouldRegisterISerializor()
  {
    // Arrange
    var services = Substitute.For<IServiceCollection>();
    var context = new SerilizationContext();

    // Act
    context.RegisterServices(services);

    // Assert
    services.Received().Add(Arg.Is<ServiceDescriptor>(sd =>
      sd.ServiceType == typeof(ISerializor) &&
      sd.ImplementationType == typeof(JsonSerializor) &&
      sd.Lifetime == ServiceLifetime.Scoped));
  }

  [Fact]
  public void AutoRegisterServices_GivenServiceType_ShouldRegisterImplementingTypes()
  {
    // Arrange
    var services = Substitute.For<IServiceCollection>();
    var context = new SerilizationContext();
    var serviceType = typeof(IJsonConverter);

    // Act
    context.RegisterServices(services);

    // Assert
    var assembly = Assembly.GetExecutingAssembly();
    var implementingTypes = assembly.GetTypes()
      .Where(type => serviceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

    foreach (var implementingType in implementingTypes)
    {
      services.Received().Add(Arg.Is<ServiceDescriptor>(sd =>
        sd.ServiceType == serviceType &&
        sd.ImplementationType == implementingType &&
        sd.Lifetime == ServiceLifetime.Scoped));
    }
  }
}