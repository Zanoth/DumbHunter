namespace SharedDataModels.Abstractions.Gear.Kinsect;

public record KinsectId(string Id) : IGearId
{
    public static KinsectId Empty() => new(string.Empty);
    public static KinsectId New(string idStr) => new(idStr);
}