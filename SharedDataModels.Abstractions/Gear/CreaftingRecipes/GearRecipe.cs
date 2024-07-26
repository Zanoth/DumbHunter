using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions.Gear.CreaftingRecipes;

public abstract record GearRecipe(GearRecipeId RecipeId, IGearId AssociatedGearId, IGearId IdOfPrevious, CraftingType CraftingType, IDictionary<ItemId, int> Items);