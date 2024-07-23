using SharedDataModels.Abstractions.Gear.CreaftingRecipes;
using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions.Gear.Charms;

public record CharmRecipe : GearRecipe
{
  public CharmRecipe(
       GearRecipeId recipeId,
       CharmId associatedCharm,
       CharmId idOfPrevious,
       CraftingType craftingType,
       IDictionary<ItemId, int> items
       )
    : base(
           recipeId,
           associatedCharm,
           idOfPrevious,
           craftingType,
           items)
  { }
}
