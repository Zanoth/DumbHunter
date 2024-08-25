namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Coatings;

public class Coating
{
    public Coating(CoatingType type, bool enabled)
    {
        Type = type;
        Enabled = enabled;
    }

    public CoatingType Type { get; }
    public bool Enabled { get; }
}