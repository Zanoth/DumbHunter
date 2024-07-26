namespace SharedDataModels.Abstractions.Gear.CreaftingRecipes;

public record GearRecipeId(string Id) : IStrongId
{
  public static GearRecipeId Empty() => new(string.Empty);
  public static GearRecipeId New(string idStr) => new(idStr);
}
