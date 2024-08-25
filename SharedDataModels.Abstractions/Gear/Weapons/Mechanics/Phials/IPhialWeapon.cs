namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Phials;

public interface IPhialWeapon : ISharpnessWeapon
{
    PhialMechanic PhialMechanic { get; }
}