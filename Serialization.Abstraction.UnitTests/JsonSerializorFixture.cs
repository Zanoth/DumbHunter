using System.Reflection;

namespace Serialization.Abstraction.UnitTests;

public class JsonSerializorFixture
{
  [Fact]
  public void Serialize_GivenObject_ShouldReturnJsonString()
  {
    // Arrange
    var jsonSerializor = new JsonSerializor(Enumerable.Empty<IJsonConverter>());
    var sampleObject = new { Name = "Test", Value = 123 };

    // Act
    var jsonString = jsonSerializor.Serialize(sampleObject);

    // Assert
    jsonString.Should().Be("{\"Name\":\"Test\",\"Value\":123}");
  }

  [Fact]
  public void Deserialize_GivenValidResource_ShouldReturnObject()
  {
    // Arrange
    var jsonSerializor = new JsonSerializor(Enumerable.Empty<IJsonConverter>());
    var mockAssembly = Substitute.For<Assembly>();
    var resourceName = "test.json";
    var jsonContent = "{\"Name\":\"Test\",\"Value\":123}";
    var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonContent));
    mockAssembly.GetManifestResourceStream(resourceName).Returns(stream);

    // Act
    var result = jsonSerializor.Deserialize<SampleClass>(mockAssembly, resourceName);

    // Assert
    result.Should().BeEquivalentTo(new SampleClass { Name = "Test", Value = 123 });
  }


  #region Helper Class(es)
  private class SampleClass
  {
    public string Name { get; set; }
    public int Value { get; set; }
  }
  #endregion
}