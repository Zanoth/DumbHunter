using System.Text;

namespace Serialization.Abstraction.UnitTests.Converters;

public class DropJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnDrop()
  {
    // Arrange
    var builder = new DropTestBuilder();
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
    var converter = new DropJsonConverter();

    // Act
    var drop = converter.Read(ref reader, typeof(Drop), new JsonSerializerOptions());

    // Assert
    var expectedDrop = builder.Build();
    drop.Should().BeEquivalentTo(expectedDrop);
  }

  [Fact]
  public void Write_GivenDrop_ShouldWriteJson()
  {
    // Arrange
    var builder = new DropTestBuilder();
    var drop = builder.Build();
    var options = new JsonSerializerOptions { Converters = { new DropJsonConverter() } };
    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, drop, options);
    writer.Flush();
    var json = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedJson = builder.BuildJson();
    json.Should().Be(expectedJson);
  }

  #region Helper Class(es)

  private class DropTestBuilder
  {
    private QuestRank _questRank = QuestRank.Undefined;
    private DropCondition _dropCondition = DropCondition.Undefined;
    private LootDetails _lootDetails = new(new ItemId("item-0123"), Stack: 10, Percentage: 50);

    public DropTestBuilder WithItemId(string itemId)
    {
      _lootDetails = _lootDetails with { ItemId = ItemId.New(itemId) };
      return this;
    }

    public DropTestBuilder WithRank(QuestRank questRank)
    {
      _questRank = questRank;
      return this;
    }

    public DropTestBuilder WithDropCondition(DropCondition dropCondition)
    {
      _dropCondition = dropCondition;
      return this;
    }

    public DropTestBuilder WithStack(int stack)
    {
      _lootDetails = _lootDetails with { Stack = stack };
      return this;
    }

    public DropTestBuilder WithPercentage(int percentage)
    {
      _lootDetails = _lootDetails with { Percentage = percentage};
      return this;
    }

    public Drop Build()
    {
      return new Drop(_questRank, _dropCondition, _lootDetails);
    }

    public string BuildJson()
    {
      var json = $@"
        {{
          ""{nameof(Drop.LootDetails.ItemId)}"": ""{_lootDetails.ItemId}"",
          ""{nameof(Drop.DropCondition)}"": ""{_dropCondition}"",          
          ""{nameof(Drop.QuestRank)}"": ""{_questRank}"",
          ""{nameof(Drop.LootDetails.Stack)}"": {_lootDetails.Stack},
          ""{nameof(Drop.LootDetails.Percentage)}"": {_lootDetails.Percentage}
        }}";

      json = TestingUtilities.MinifyJson(json);

      return json;
    }
  }

  #endregion
}