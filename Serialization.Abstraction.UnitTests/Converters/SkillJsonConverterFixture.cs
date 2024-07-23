using System.Drawing;
using System.Text;

namespace Serialization.Abstraction.UnitTests.Converters;

public class SkillJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnSkill()
  {
    // Arrange
    var builder = new SkillTestBuilder();
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
    var converter = new SkillJsonConverter();

    // Act
    var skill = converter.Read(ref reader, typeof(Skill), new JsonSerializerOptions());

    // Assert
    var expectedSkill = builder.Build();
    skill.Should().BeEquivalentTo(expectedSkill);
  }

  [Fact]
  public void Write_GivenSkill_ShouldWriteJson()
  {
    // Arrange
    var builder = new SkillTestBuilder();
    var skill = builder.Build();

    var skillJsonConverter = new SkillJsonConverter();
    var options = new JsonSerializerOptions { Converters = { skillJsonConverter } };

    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, skill, options);
    writer.Flush();
    var json = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedJson = builder.BuildJson();

    expectedJson = TestingUtilities.MinifyJson(expectedJson);
    json = TestingUtilities.MinifyJson(json);

    json.Should().Be(expectedJson);
  }

  #region Helper Class(es)

  private class SkillTestBuilder
  {
    private SkillId _skillId = SkillId.New("SkillId-123");
    private string _name = "TestSkill";
    private Color _iconColor = Color.Violet;
    private int _secret = 2;
    private SkillId _unlocksId = SkillId.New("SkillId-321");

    private List<SkillLevel> _levels = new List<SkillLevel>
    {
      new SkillLevel(1, "Level 1 Description"),
      new SkillLevel(2, "Level 2 Description")
    };

    public SkillTestBuilder WithSkillId(string skillId)
    {
      _skillId = SkillId.New(skillId);
      return this;
    }

    public SkillTestBuilder WithName(string name)
    {
      _name = name;
      return this;
    }

    public SkillTestBuilder WithLevels(List<SkillLevel> levels)
    {
      _levels = levels;
      return this;
    }

    public Skill Build()
    {
      return new Skill(_skillId, _name, _iconColor, _secret, _unlocksId, _levels);
    }

    public string BuildJson()
    {
      var levels = string.Join(", ", _levels.Select(level => $@"
          {{ ""{nameof(SkillLevel.Level)}"": {level.Level},
             ""{nameof(SkillLevel.Description)}"": ""{level.Description}"" 
          }}"));

      var json = $@"
        {{
          ""{nameof(Skill.SkillId)}"": ""{_skillId.Id}"",
          ""{nameof(Skill.Name)}"": ""{_name}"",
          ""{nameof(Skill.IconColor)}"": ""{_iconColor.Name}"",
          ""{nameof(Skill.Secret)}"": {_secret},
          ""{nameof(Skill.UnlocksId)}"": ""{_unlocksId.Id}"",
          ""{nameof(Skill.Levels)}"": [{levels}]
        }}";

      return json;
    }
  }

  #endregion
}