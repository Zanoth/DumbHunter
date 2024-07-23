using System.Text.Json.Serialization;

namespace Serialization.Abstraction.UnitTests.Converters;

public class SkillLevelJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnSkillLevel()
  {
    // Arrange
    var builder = new SkillLevelTestBuilder();
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));
    var converter = new SkillLevelJsonConverter();

    // Act
    var skillLevel = converter.Read(ref reader, typeof(SkillLevel), new JsonSerializerOptions());

    // Assert
    var expectedSkillLevel = builder.BuildSkillLevel();
    skillLevel.Should().BeEquivalentTo(expectedSkillLevel);
  }

  [Fact]
  public void Write_GivenSkillLevel_ShouldWriteJson()
  {
    // Arrange
    var builder = new SkillLevelTestBuilder();
    var skillLevel = builder.BuildSkillLevel();

    var skillLevelConverter = new SkillLevelJsonConverter();
    var options = new JsonSerializerOptions { Converters = { skillLevelConverter } };

    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, skillLevel, options);

    writer.Flush();
    var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedJson = builder.BuildJson();

    expectedJson = TestingUtilities.MinifyJson(expectedJson);
    json = TestingUtilities.MinifyJson(json);

    json.Should().Be(expectedJson);
  }
}

#region Helper Class(es)
public class SkillLevelTestBuilder
{
  private int _level = 1;
  private string _description = "Test Description";

  public SkillLevelTestBuilder WithLevel(int level)
  {
    _level = level;
    return this;
  }

  public SkillLevelTestBuilder WithDescription(string description)
  {
    _description = description;
    return this;
  }

  public SkillLevel BuildSkillLevel()
  {
    return new SkillLevel(_level, _description);
  }

  public string BuildJson()
  {
    var json = $@"
      {{
        ""{nameof(SkillLevel.Level)}"": {_level},
        ""{nameof(SkillLevel.Description)}"": ""{_description}"" 
      }}";


    return json;
  }
}
#endregion