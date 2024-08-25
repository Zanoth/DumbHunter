namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Coatings;

public class CoatingMechanic : IWeaponMechanic
{
    public CoatingMechanic(IEnumerable<Coating> coatings) => Coatings = coatings.ToList();

    public List<Coating> Coatings { get; init; }
}