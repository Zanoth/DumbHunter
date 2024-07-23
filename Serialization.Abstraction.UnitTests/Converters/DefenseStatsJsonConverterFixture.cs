using System.Text;

namespace Serialization.Abstraction.UnitTests.Converters;

public class DefenseStatsJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnDefenseStats()
  {
    // Arrange
    var builder = new DefenseStatsTestBuilder();
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
    var converter = new DefenseStatsJsonConverter();

    // Act
    var defenseStats = converter.Read(ref reader, typeof(DefenseStats), new JsonSerializerOptions());

    // Assert
    var expectedDefenseStats = builder.Build();
    defenseStats.Should().BeEquivalentTo(expectedDefenseStats);
  }

  [Fact]
  public void Write_GivenDefenseStats_ShouldWriteJson()
  {
    // Arrange
    var builder = new DefenseStatsTestBuilder();
    var defenseStats = builder.Build();
    var options = new JsonSerializerOptions { Converters = { new DefenseStatsJsonConverter() } };
    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, defenseStats, options);
    writer.Flush();
    var json = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedJson = builder.BuildJson();
    json.Should().Be(expectedJson);
  }

  #region Helper Class(es)

  private class DefenseStatsTestBuilder
  {
    private int _baseDef = 100;
    private int _maxDef = 200;
    private int _argumentedMaxDef = 250;
    private int _fireDef = 10;
    private int _waterDef = 20;
    private int _thunderDef = 30;
    private int _iceDef = 40;
    private int _dragonDef = 50;

    public DefenseStatsTestBuilder WithBaseDef(int baseDef)
    {
      _baseDef = baseDef;
      return this;
    }

    public DefenseStatsTestBuilder WithMaxDef(int maxDef)
    {
      _maxDef = maxDef;
      return this;
    }

    public DefenseStatsTestBuilder WithArgumentedMaxDef(int argumentedMaxDef)
    {
      _argumentedMaxDef = argumentedMaxDef;
      return this;
    }

    public DefenseStatsTestBuilder WithFireDef(int fireDef)
    {
      _fireDef = fireDef;
      return this;
    }

    public DefenseStatsTestBuilder WithWaterDef(int waterDef)
    {
      _waterDef = waterDef;
      return this;
    }

    public DefenseStatsTestBuilder WithThunderDef(int thunderDef)
    {
      _thunderDef = thunderDef;
      return this;
    }

    public DefenseStatsTestBuilder WithIceDef(int iceDef)
    {
      _iceDef = iceDef;
      return this;
    }

    public DefenseStatsTestBuilder WithDragonDef(int dragonDef)
    {
      _dragonDef = dragonDef;
      return this;
    }

    public DefenseStats Build()
    {
      return new DefenseStats(_baseDef, _maxDef, _argumentedMaxDef, _fireDef, _waterDef, _thunderDef, _iceDef, _dragonDef);
    }

    public string BuildJson()
    {
      var json = $@"
        {{
          ""{nameof(DefenseStats.BaseDef)}"": {_baseDef},
          ""{nameof(DefenseStats.MaxDef)}"": {_maxDef},
          ""{nameof(DefenseStats.ArgumentedMaxDef)}"": {_argumentedMaxDef},
          ""{nameof(DefenseStats.FireDef)}"": {_fireDef},
          ""{nameof(DefenseStats.WaterDef)}"": {_waterDef},
          ""{nameof(DefenseStats.ThunderDef)}"": {_thunderDef},
          ""{nameof(DefenseStats.IceDef)}"": {_iceDef},
          ""{nameof(DefenseStats.DragonDef)}"": {_dragonDef}
        }}";

      json = TestingUtilities.MinifyJson(json);
      return json;
    }
  }

  #endregion
}