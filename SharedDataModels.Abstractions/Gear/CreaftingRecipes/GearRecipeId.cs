namespace SharedDataModels.Abstractions.Gear.CreaftingRecipes;

public record GearRecipeId(string Id) : IStrongId<string>
{
  public static GearRecipeId Empty() => new(string.Empty);
  public static GearRecipeId New(string idStr) => new(idStr);
}
