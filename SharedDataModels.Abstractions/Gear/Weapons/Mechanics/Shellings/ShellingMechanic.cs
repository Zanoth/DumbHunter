using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Shellings;

public class ShellingMechanic : IWeaponMechanic
{
    public ShellingMechanic(ShellingType shellingType, int shellingLevel)
    {
        ShellingType = shellingType;
        ShellingLevel = shellingLevel;
    }

    public ShellingType ShellingType { get; init; }
    public int ShellingLevel { get; init; }
}