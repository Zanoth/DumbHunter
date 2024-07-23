using System.Drawing;
using System.Text;

namespace Serialization.Abstraction.UnitTests.Converters;

public class ItemJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnItem()
  {
    // Arrange
    var builder = new ItemTestBuilder();
    var json = builder.BuildJson();

    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

    var itemConverter = new ItemJsonConverter();
    var jsonSerializerOptions = new JsonSerializerOptions { Converters = { itemConverter } };

    // Act
    var item = itemConverter.Read(ref reader, typeof(Item), jsonSerializerOptions);

    // Assert
    var expectedItem = builder.BuildItem();
    item.Should().BeEquivalentTo(expectedItem);
  }

  [Fact]
  public void Write_GivenItem_ShouldWriteJson()
  {
    // Arrange
    var builder = new ItemTestBuilder();
    var item = builder.BuildItem();

    var itemJsonConverter = new ItemJsonConverter();
    var options = new JsonSerializerOptions { Converters = { itemJsonConverter } };

    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, item, options);
    writer.Flush();
    var json = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedJson = builder.BuildJson();

    json = TestingUtilities.MinifyJson(json);
    expectedJson = TestingUtilities.MinifyJson(expectedJson);

    json.Should().Be(expectedJson);
  }



  #region Helper Class(es)

  private class ItemTestBuilder
  {
    private string _itemId = "item-123";
    private string _name = "TestItem";
    private string _category = "TestCategory";
    private string _subcategory = "TestSubcategory";
    private int _rarity = 5;
    private int _buyPrice = 100;
    private int _sellPrice = 50;
    private int _carryLimit = 10;
    private int _points = 20;
    private string _iconName = "TestIcon";
    private Color _iconColor = Color.Red;

    public ItemTestBuilder WithItemId(string itemId)
    {
      _itemId = itemId;
      return this;
    }

    public ItemTestBuilder WithName(string name)
    {
      _name = name;
      return this;
    }

    public ItemTestBuilder WithCategory(string category)
    {
      _category = category;
      return this;
    }

    public ItemTestBuilder WithSubcategory(string subcategory)
    {
      _subcategory = subcategory;
      return this;
    }

    public ItemTestBuilder WithRarity(int rarity)
    {
      _rarity = rarity;
      return this;
    }

    public ItemTestBuilder WithBuyPrice(int buyPrice)
    {
      _buyPrice = buyPrice;
      return this;
    }

    public ItemTestBuilder WithSellPrice(int sellPrice)
    {
      _sellPrice = sellPrice;
      return this;
    }

    public ItemTestBuilder WithCarryLimit(int carryLimit)
    {
      _carryLimit = carryLimit;
      return this;
    }

    public ItemTestBuilder WithPoints(int points)
    {
      _points = points;
      return this;
    }

    public ItemTestBuilder WithIconName(string iconName)
    {
      _iconName = iconName;
      return this;
    }

    public ItemTestBuilder WithIconColor(Color iconColor)
    {
      _iconColor = iconColor;
      return this;
    }

    public Item BuildItem()
    {
      return new Item(
        ItemId.New(_itemId),
        _name,
        _category,
        _subcategory,
        _rarity,
        _buyPrice,
        _sellPrice,
        _carryLimit,
        _points,
        _iconName,
        _iconColor
      );
    }

    public string BuildJson()
    {
      var json = $@"
  {{
    ""{nameof(Item.ItemId)}"": ""{_itemId}"",
    ""{nameof(Item.Name)}"": ""{_name}"",
    ""{nameof(Item.Category)}"": ""{_category}"",
    ""{nameof(Item.Subcategory)}"": ""{_subcategory}"",
    ""{nameof(Item.Rarity)}"": {_rarity},
    ""{nameof(Item.BuyPrice)}"": {_buyPrice},
    ""{nameof(Item.SellPrice)}"": {_sellPrice},
    ""{nameof(Item.CarryLimit)}"":  {_carryLimit},
    ""{nameof(Item.Points)}"":  {_points},
    ""{nameof(Item.IconName)}"": ""{_iconName}"",
    ""{nameof(Item.IconColor)}"": ""{_iconColor.Name}""
  }}";
      return json;
    }
  }

  #endregion
}