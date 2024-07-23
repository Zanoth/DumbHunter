using SharedDataModels.Abstractions.Gear.Charms;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class GearRecipeJsonConverter : JsonConverter<GearRecipe>, IJsonConverter
{
  public override GearRecipe Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var recipeId = GearRecipeId.New(root.GetProperty(nameof(GearRecipe.RecipeId)).GetString());
    var associatedGearId = root.GetProperty(nameof(GearRecipe.AssociatedGearId)).GetString();
    var idOfPrevious = root.GetProperty(nameof(GearRecipe.IdOfPrevious)).GetString();
    var craftingType = (CraftingType)Enum.Parse(typeof(CraftingType), root.GetProperty(nameof(GearRecipe.CraftingType)).ToString());

    var items = new Dictionary<ItemId, int>();
    var itemsArray = root.GetProperty(nameof(GearRecipe.Items)).EnumerateArray();
    while (itemsArray.MoveNext())
    {
      var itemJson = itemsArray.Current;

      var itemId = ItemId.New(itemJson.GetProperty(nameof(Item.ItemId)).GetString());
      var quantity = itemJson.GetProperty("Quantity").GetInt32();

      items.Add(itemId, quantity);
    }

    // Determine the type based on the Id
    if (associatedGearId.Contains("armor"))
    {
      var associatedArmor = ArmorId.New(associatedGearId);
      var armorRecipe = new ArmorRecipe(recipeId, associatedArmor, items);
      return armorRecipe;
    }
    else if (associatedGearId.Contains("weapon"))
    {
      var associatedWeapon = WeaponId.New(associatedGearId);
      var previousWeapon = WeaponId.New(idOfPrevious);
      var weaponRecipe = new WeaponRecipe(recipeId, associatedWeapon, previousWeapon, craftingType, items);
      return weaponRecipe;
    }
    else if (associatedGearId.Contains("charm"))
    {
      var associatedCharm = CharmId.New(associatedGearId);
      var previousCharm = CharmId.New(idOfPrevious);
      var charmRecipe = new CharmRecipe(recipeId, associatedCharm, previousCharm, craftingType, items);
      return charmRecipe;
    }

    throw new InvalidOperationException($"Unknown gear type in ID: {associatedGearId}");
  }

  public override void Write(Utf8JsonWriter writer, GearRecipe recipe, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(GearRecipe.RecipeId), recipe.RecipeId.Id);
    writer.WriteString(nameof(GearRecipe.AssociatedGearId), recipe.AssociatedGearId.Id);
    writer.WriteString(nameof(GearRecipe.IdOfPrevious), recipe.IdOfPrevious.Id);
    writer.WriteString(nameof(GearRecipe.CraftingType), recipe.CraftingType.ToString());

    writer.WriteStartArray("Items");
    foreach (var item in recipe.Items)
    {
      writer.WriteStartObject();
      writer.WriteString("ArmorId", item.Key.Id);
      writer.WriteNumber("Quantity", item.Value);
      writer.WriteEndObject();
    }
    writer.WriteEndArray();

    writer.WriteEndObject();
  }
}
