using SharedDataModels.Abstractions.Gear.CreaftingRecipes;
using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions.Gear.Armors;

public record ArmorRecipe : GearRecipe
{
  public ArmorRecipe(GearRecipeId recipeId, ArmorId associatedArmor,
    IDictionary<ItemId, int> items) : base(recipeId, associatedArmor,
    ArmorId.Empty(),
    CraftingType.Create,
    items) 
    { }
    
}