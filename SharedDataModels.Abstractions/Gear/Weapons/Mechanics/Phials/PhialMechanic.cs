using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Phials;

public class PhialMechanic : IWeaponMechanic
{
    public PhialMechanic(PhialType phialType, int phialPower)
    {
        PhialType = phialType;
        PhialPower = phialPower;
    }

    public PhialType PhialType { get; init; }
    public int PhialPower { get; init; }      // TODO: it looks like some of phial power entries are set to -1 when they should have an actual value
}