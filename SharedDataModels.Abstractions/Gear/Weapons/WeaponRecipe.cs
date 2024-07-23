using SharedDataModels.Abstractions.Gear.CreaftingRecipes;
using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions.Gear.Weapons;

public record WeaponRecipe : GearRecipe
{
  public WeaponRecipe(
    GearRecipeId recipeId,
    WeaponId associatedWeapon,
    WeaponId idOfPrevious,
    CraftingType craftingType,
    IDictionary<ItemId, int> items
    )
    : base(
      recipeId,
      associatedWeapon,
      idOfPrevious,
      craftingType,
      items)
  { }
}