using SharedDataModels.Abstractions.Gear.CreaftingRecipes;
using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions.Gear.Kinsect;

public record KinsectRecipe : GearRecipe
{
  public KinsectRecipe(
    GearRecipeId recipeId,
    KinsectId associatedKinsect,
    KinsectId idOfPrevious,
    CraftingType craftingType,
    IDictionary<ItemId, int> items
  )
    : base(
      recipeId,
      associatedKinsect,
      idOfPrevious,
      craftingType,
      items)
  { }
}