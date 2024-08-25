namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Coatings;

public interface ICoatingWeapon : IWeapon
{
    CoatingMechanic CoatingMechanic { get; }
}