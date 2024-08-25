using Prism.DryIoc;
using System.Reflection;


namespace Serialization.Abstraction.UnitTests;

public class SerilizationContextFixture
{
  [Fact]
  public void RegisterServices_GivenServiceCollection_ShouldRegisterISerializor()
  {
    throw new NotImplementedException();

    //// Arrange
    //var iocContainer = new DryIocContainerExtension();
    //var typeFinder = new TestTypeFinder();
    //var context = new SerilizationContext(typeFinder);

    //var serviceDescriptor = new ServiceDescriptor(typeof(ISerializor), typeof(TestSerializorMock), ServiceLifetime.Scoped);

    //// Act
    //context.RegisterServices(iocContainer);

    //// Assert
    //iocContainer
    //  .Should()
    //  .ContainSingle(sd => sd.ServiceType == serviceDescriptor.ServiceType);
  }

  [Fact]
  public void AutoRegisterServices_GivenServiceType_ShouldRegisterImplementingTypes()
  {
    //throw new NotImplementedException();

    //// Arrange
    //var serviceCollection = new ServiceCollection();
    //var typeFinder = new TestTypeFinder();
    //var context = new SerilizationContext(typeFinder);

    //var serviceDescriptor = new ServiceDescriptor(typeof(IJsonConverter), typeof(TestJsonConverter), ServiceLifetime.Scoped);

    //// Act
    //context.RegisterServices(serviceCollection);

    //// Assert
    //serviceCollection
    //  .Should()
    //  .ContainSingle(sd => sd.ServiceType == serviceDescriptor.ServiceType
    //                    && sd.ImplementationType == serviceDescriptor.ImplementationType
    //                    && sd.Lifetime == serviceDescriptor.Lifetime);
    
  }

  #region Helper class(es)
  public class TestTypeFinder : ITypeFinder
  {
    public IEnumerable<Type> GetImplementingTypes(Type serviceType)
    {
      // Return a hardcoded list of types that you want to register in your tests
      return new List<Type> { typeof(TestJsonConverter) };
    }
  }


  public class TestJsonConverter : IJsonConverter
  {
  }
  
  public class TestSerializorMock : ISerializor // This class is used to verify that the ISerializor is registered, regardless of the implementation class
  {
    public string Serialize<T>(T obj)
    {
      throw new NotImplementedException();
    }

    public T Deserialize<T>(Assembly resourceAssembly, string resourceName, JsonSerializerOptions? options = null)
    {
      throw new NotImplementedException();
    }
  }
  #endregion
}
