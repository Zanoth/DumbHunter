using Serialization.Abstraction;

namespace MHW.DataModels.Gear.CraftingRecipes;
public class GearRecipeRepository : RepositoryBase<GearRecipeId, GearRecipe>
{
  private static readonly string ResourceName = $"{typeof(GearRecipeRepository).Namespace}.GearRecipeConfiguration.json";

  public GearRecipeRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize(ResourceName);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<GearRecipeId, GearRecipe> entityDictionary, List<GearRecipe> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.RecipeId, entity);
  }
}