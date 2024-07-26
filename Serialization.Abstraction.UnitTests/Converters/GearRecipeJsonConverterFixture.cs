using SharedDataModels.Abstractions.Items;
using System.Text;
using SharedDataModels.Abstractions.Gear.Charms;

namespace Serialization.Abstraction.UnitTests.Converters;

public class GearRecipeJsonConverterFixture
{
  private readonly GearRecipeJsonConverter _converter;
  private readonly JsonSerializerOptions _options;

  public GearRecipeJsonConverterFixture()
  {
    _converter = new GearRecipeJsonConverter();
    _options = new JsonSerializerOptions { Converters = { _converter } };
  }

  [Fact]
  public void Read_GivenValidJson_ShouldReturnGearRecipe()
  {
    // Arrange
    var builder = new GearRecipeTestBuilder();
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

    // Act
    var gearRecipe = _converter.Read(ref reader, typeof(GearRecipe), _options);

    // Assert
    var expectedGearRecipe = builder.BuildGearRecipe();
    gearRecipe.Should().BeEquivalentTo(expectedGearRecipe);
  }

  [Fact]
  public void Write_GivenGearRecipe_ShouldWriteJson()
  {
    // Arrange
    var builder = new GearRecipeTestBuilder();
    var gearRecipe = builder.BuildGearRecipe();

    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, gearRecipe, _options);
    writer.Flush();
    var json = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    var expectedJson = builder.BuildJson();
    json.Should().Be(expectedJson);
  }

  [Fact]
  public void Read_GivenArmorId_WhenCalled_ReturnsArmorRecipe()
  {
    // Arrange
    var builder = new GearRecipeTestBuilder().WithAssociatedGearId(ArmorId.New("armor-456"));
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

    // Act
    var result = _converter.Read(ref reader, typeof(GearRecipe), _options);

    // Assert
    var expectedGearRecipe = builder.BuildGearRecipe();
    result.Should().BeEquivalentTo(expectedGearRecipe);
  }

  [Fact]
  public void Read_GivenWeaponId_WhenCalled_ReturnsWeaponRecipe()
  {
    // Arrange
    var builder = new GearRecipeTestBuilder()
      .WithAssociatedGearId(WeaponId.New("weapon-456"));
    var json = builder.BuildJson();
    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

    // Act
    var result = _converter.Read(ref reader, typeof(GearRecipe), _options);

    // Assert
    var expectedGearRecipe = builder.BuildGearRecipe();
    result.Should().BeEquivalentTo(expectedGearRecipe);
  }

  [Fact]
  public void Read_GivenCharmId_WhenCalled_ThrowsNotImplementedException()
  {
    // Arrange
    var builder = new GearRecipeTestBuilder().WithAssociatedGearId(CharmId.New("charm-456"));
    var json = builder.BuildJson();
    

    // Act
    Action act = () =>
    {
      var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
      _converter.Read(ref reader, typeof(GearRecipe), _options);
    };

    // Assert
    act.Should().Throw<NotImplementedException>().WithMessage("Charm recipe not implemented");
  }

  #region Helper Class(es)

  private class GearRecipeTestBuilder
  {
    private GearRecipeId _recipeId = GearRecipeId.New("recipe-03");
    private IGearId _associatedGearId = ArmorId.New("armor-0123");
    private IGearId _idOfPrevious = ArmorId.Empty();
    private CraftingType _craftingType = CraftingType.Create;
    private Dictionary<ItemId, int> _items = new Dictionary<ItemId, int>
    {
      { ItemId.New("item-0001"), 10 },
      { ItemId.New("item-0002"), 20 }
    };

    public GearRecipeTestBuilder WithAssociatedGearId(IGearId associatedGearId)
    {
      _associatedGearId = associatedGearId;
      return this;
    }
    public GearRecipeTestBuilder WithUpgradeableRecipe(IGearId idOfPrevious)
    {
      if (idOfPrevious is ArmorId armorId)
        throw new ArgumentException("Armor cannot be upgraded");

      _idOfPrevious = idOfPrevious;
      _craftingType = CraftingType.Upgrade;
      return this;
    }
    
    //public GearRecipeTestBuilder WithIdOfPrevious(IGearId<string> idOfPrevious)
    //{
    //  _idOfPrevious = idOfPrevious;
    //  return this;
    //}

    //public GearRecipeTestBuilder WithCraftingType(CraftingType craftingType)
    //{
    //  _craftingType = craftingType;
    //  return this;
    //}

    public GearRecipeTestBuilder WithItems(Dictionary<ItemId, int> items)
    {
      _items = items;
      return this;
    }

    public GearRecipe BuildGearRecipe()
    {
      if (_associatedGearId is ArmorId armorId)
      {
        var armorRecipe = new ArmorRecipe(_recipeId, armorId, _items);
        return armorRecipe;
      }
      else if (_associatedGearId is WeaponId weaponId)
      {
        if (_idOfPrevious is not WeaponId)
        {
          if (_craftingType == CraftingType.Upgrade)
            throw new ArgumentException($"To upgrade weapons, the idOfPrevious must be of type{typeof(WeaponId)}");
          
          _idOfPrevious = WeaponId.Empty();
        }

        if (_craftingType == CraftingType.Upgrade && _idOfPrevious is not WeaponId)
          throw new ArgumentException($"To upgrade weapons, the idOfPrevious must be of type{typeof(WeaponId)}");
        if (_idOfPrevious is not WeaponId)
          _idOfPrevious = WeaponId.Empty();

        var weaponRecipe = new WeaponRecipe(_recipeId, weaponId, (WeaponId) _idOfPrevious, _craftingType, _items);
        return weaponRecipe;
      }
      else
      {
        throw new InvalidOperationException($"Unknown gear type in ID: {_associatedGearId}");
      }
    }

    public string BuildJson()
    {
      var items = string.Join(", ", _items.Select(item => $@"
        {{
          ""{nameof(ItemId.Id)}"": ""{item.Key.Id}"",
          ""Quantity"": {item.Value}
        }}"));

      var json = $@"
      {{
        ""{nameof(GearRecipe.RecipeId)}"": ""{_recipeId.Id}"",
        ""{nameof(GearRecipe.AssociatedGearId)}"": ""{_associatedGearId.Id}"",
        ""{nameof(GearRecipe.IdOfPrevious)}"": ""{_idOfPrevious.Id}"",
        ""{nameof(GearRecipe.CraftingType)}"": ""{_craftingType}"",
        ""{nameof(GearRecipe.Items)}"": [{items}]
      }}";

      json = TestingUtilities.MinifyJson(json);
      return json;
    }
  }

  #endregion
}