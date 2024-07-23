using SharedDataModels.Abstractions.Monsters;
using System.Drawing;
using System.Text;

namespace Serialization.Abstraction.UnitTests.Converters;

public class SharpnessJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnSharpness()
  {
    // Arrange
    var builder = new SharpnessTestBuilder();

    var expectedSharpness = builder.BuildSharpness();
    var sharpnessJson = builder.BuildJson();

    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(sharpnessJson));

    var sharpnessConverter = new SharpnessJsonConverter();
    var options = new JsonSerializerOptions { Converters = { sharpnessConverter } };

    // Act
    var sharpness = sharpnessConverter.Read(ref reader, typeof(Sharpness), options);

    // Assert
    sharpness.Should().BeEquivalentTo(expectedSharpness);
  }

  [Fact]
  public void Write_GivenSharpness_ShouldWriteJson()
  {
    // Arrange
    var builder = new SharpnessTestBuilder();

    var sharpness = builder.BuildSharpness();


    var options = new JsonSerializerOptions { Converters = { new SharpnessJsonConverter() } };

    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, sharpness, options);
    writer.Flush();
    var sharpnessJson = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedSharpnessJson = builder.BuildJson();

    sharpnessJson = TestingUtilities.MinifyJson(sharpnessJson);
    expectedSharpnessJson = TestingUtilities.MinifyJson(expectedSharpnessJson);

    sharpnessJson.Should().Be(expectedSharpnessJson);
  }

  #region Helper Class(es)

  public class SharpnessTestBuilder
  {
    private bool _isMaxed = false;
    private List<SharpnessSection> _sections = new()
    {
      new SharpnessSection(Color.Red, 30),
      new SharpnessSection(Color.Orange, 60),
      new SharpnessSection(Color.Yellow, 20),
      new SharpnessSection(Color.Green, 140),
      new SharpnessSection(Color.Blue, 70),
      new SharpnessSection(Color.White, 35),
      new SharpnessSection(Color.Purple, 12)
    };

    public SharpnessTestBuilder WithIsMaxed(bool isMaxed)
    {
      _isMaxed = isMaxed;
      return this;
    }

    public SharpnessTestBuilder WithSections(List<SharpnessSection> sections)
    {
      _sections = sections;
      return this;
    }

    public Sharpness BuildSharpness()
    {
      return new Sharpness(_isMaxed, _sections);
    }
    public string BuildJson()
    {
      var sectionsJson = string.Join(", ", _sections.Select(section => $@"
        {{
          ""{nameof(SharpnessSection.Color)}"": ""{section.Color.Name}"",
          ""{nameof(SharpnessSection.Value)}"": {section.Value}
        }}"));

      var json = $@"
      {{
        ""{nameof(Sharpness.IsMaxed)}"": {_isMaxed.ToString().ToLower()},
        ""{nameof(Sharpness.Sections)}"": [{sectionsJson}]
      }}";

      return json;
    }
  }

  #endregion
}