namespace SharedDataModels.Abstractions.Skills;

public record SkillId(string Id) : IStrongId<string>
{
  public static SkillId Empty() => new(string.Empty);
  public static SkillId New(string idStr) => new(idStr);
}