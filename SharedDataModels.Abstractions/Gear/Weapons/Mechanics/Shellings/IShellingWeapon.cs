namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Shellings;

public interface IShellingWeapon : ISharpnessWeapon
{
    public ShellingMechanic ShellingMechanic { get; }
}