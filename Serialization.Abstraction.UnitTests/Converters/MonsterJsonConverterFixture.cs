using System.Text;

namespace Serialization.Abstraction.UnitTests.Converters;

public class MonsterJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnMonster()
  {
    // Arrange
    var builder = new MonsterTestBuilder();
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

    var monsterConverter = new MonsterJsonConverter();
    var dropConverter = new DropJsonConverter();

    var jsonSerializerOptions = new JsonSerializerOptions { Converters = { monsterConverter, dropConverter } };

    // Act
    var monster = monsterConverter.Read(ref reader, typeof(Monster), jsonSerializerOptions);

    // Assert
    var expectedMonster = builder.BuildMonster();
    monster.Should().BeEquivalentTo(expectedMonster);
  }

  [Fact]
  public void Write_GivenMonster_ShouldWriteJson()
  {
    // Arrange
    var builder = new MonsterTestBuilder();
    var monster = builder.BuildMonster();

    var monsterConverter = new MonsterJsonConverter();
    var dropConverter = new DropJsonConverter();

    var options = new JsonSerializerOptions { Converters = { monsterConverter, dropConverter } };

    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, monster, options);

    writer.Flush();
    var json = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedJson = builder.BuildJson();

    json = TestingUtilities.MinifyJson(json);
    expectedJson = TestingUtilities.MinifyJson(expectedJson);

    json.Should().Be(expectedJson);
  }



  #region Helper Class(es)

  private class MonsterTestBuilder
  {
    private MonsterId _monsterId = MonsterId.New("MonsterId-123");
    private string _name = "TestMonster";
    private MonsterEcology _ecology = MonsterEcology.Undefined;
    private MonsterSize _size = MonsterSize.Large;
    private TrapInteractions _trapInteractions = new TrapInteractions(pitfallTrap: true, shockTrap: false, vineTrap: true);


    private IEnumerable<Drop> _drops = new List<Drop>
    {
      new Drop(QuestRank.Undefined, DropCondition.Undefined, new LootDetails(ItemId.New("ItemId-0001"), 1, 100)),
      new Drop(QuestRank.Undefined, DropCondition.Undefined, new LootDetails(ItemId.New("ItemId-0002"), 1, 100))
    };

    public MonsterTestBuilder WithMonsterId(MonsterId monsterId)
    {
      _monsterId = monsterId;
      return this;
    }

    public MonsterTestBuilder WithName(string name)
    {
      _name = name;
      return this;
    }

    public MonsterTestBuilder WithEcology(MonsterEcology ecology)
    {
      _ecology = ecology;
      return this;
    }

    public MonsterTestBuilder WithSize(MonsterSize size)
    {
      _size = size;
      return this;
    }
    public MonsterTestBuilder WithTrapInteractions(TrapInteractions trapInteractions)
    {
      _trapInteractions = trapInteractions;
      return this;
    }

    public MonsterTestBuilder WithDrops(IEnumerable<Drop> drops)
    {
      _drops = drops;
      return this;
    }

    public Monster BuildMonster()
    {
      return new Monster(
        _monsterId,
        _name,
        _ecology,
        _size,
        _trapInteractions,
        _drops
      );
    }

    public string BuildJson()
    {
      var dropsJson = string.Join(", ", _drops.Select(drop => $@"
        {{
          ""{nameof(Drop.LootDetails.ItemId)}"": ""{drop.LootDetails.ItemId.Id}"",
          ""{nameof(Drop.DropCondition)}"": ""{drop.DropCondition}"",          
          ""{nameof(Drop.QuestRank)}"": ""{drop.QuestRank}"",
          ""{nameof(Drop.LootDetails.Stack)}"": {drop.LootDetails.Stack},
          ""{nameof(Drop.LootDetails.Percentage)}"": {drop.LootDetails.Percentage}
        }}"));

      var json = $@"
      {{
        ""{nameof(Monster.MonsterId)}"": ""{_monsterId.Id}"",
        ""{nameof(Monster.Name)}"": ""{_name}"",
        ""{nameof(Monster.Ecology)}"": ""{_ecology.ToString()}"",
        ""{nameof(Monster.Size)}"": ""{_size.ToString()}"",
        ""{nameof(Monster.TrapInteractions.PitfallTrap)}"": {_trapInteractions.PitfallTrap.ToString().ToLower()},
        ""{nameof(Monster.TrapInteractions.ShockTrap)}"": {_trapInteractions.ShockTrap.ToString().ToLower()},
        ""{nameof(Monster.TrapInteractions.VineTrap)}"": {_trapInteractions.VineTrap.ToString().ToLower()},
        ""{nameof(Monster.Drops)}"": [{dropsJson}]
      }}";

      return json;
    }
  }

  #endregion
}