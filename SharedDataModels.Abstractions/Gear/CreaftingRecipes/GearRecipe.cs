using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions.Gear.CreaftingRecipes;

public abstract record GearRecipe(GearRecipeId RecipeId, IGearId<string> AssociatedGearId, IGearId<string> IdOfPrevious, CraftingType CraftingType, IDictionary<ItemId, int> Items);