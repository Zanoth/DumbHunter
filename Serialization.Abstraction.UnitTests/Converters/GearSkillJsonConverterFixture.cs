//using SharedDataModels.Abstractions.Skills;
//using System.Text;

//namespace Serialization.Abstraction.UnitTests.Converters;

//public class GearSkillJsonConverterFixture
//{
//  [Fact]
//  public void Read_GivenValidJson_ShouldReturnGearSkill()
//  {
//    // Arrange
//    var builder = new GearSkillTestBuilder();
//    var json = builder.BuildJson();
//    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
//    var converter = new GearSkillJsonConverter();

//    // Act
//    var gearSkill = converter.Read(ref reader, typeof(GearSkill), new JsonSerializerOptions());

//    // Assert
//    var expectedGearSkill = builder.Build();
//    gearSkill.Should().BeEquivalentTo(expectedGearSkill);
//  }

//  [Fact]
//  public void Write_GivenGearSkill_ShouldWriteJson()
//  {
//    // Arrange
//    var builder = new GearSkillTestBuilder();
//    var gearSkill = builder.Build();
//    var options = new JsonSerializerOptions { Converters = { new GearSkillJsonConverter() } };
//    var stream = new MemoryStream();
//    var writer = new Utf8JsonWriter(stream);

//    // Act
//    JsonSerializer.Serialize(writer, gearSkill, options);
//    writer.Flush();
//    var json = Encoding.UTF8.GetString(stream.ToArray());

//    // Assert
//    var expectedJson = builder.BuildJson();
//    json.Should().Be(expectedJson);
//  }

//  #region Helper Class(es)

//  private class GearSkillTestBuilder
//  {
//    private string _skillId = "Skill-123";
//    private int _skillLevel = 5;

//    public GearSkillTestBuilder WithSkillId(string skillId)
//    {
//      _skillId = skillId;
//      return this;
//    }

//    public GearSkillTestBuilder WithSkillLevel(int skillLevel)
//    {
//      _skillLevel = skillLevel;
//      return this;
//    }

//    public GearSkill Build()
//    {
//      return new GearSkill(SkillId.New(_skillId), _skillLevel);
//    }

//    public string BuildJson()
//    {
//      var json = $@"
//        {{
//          ""{nameof(GearSkill.SkillId)}"": ""{_skillId}"",
//          ""{nameof(GearSkill.SkillLevel)}"": {_skillLevel}
//        }}";

//      json = TestingUtilities.MinifyJson(json);

//      return json;
//    }
//  }

//  #endregion
//}