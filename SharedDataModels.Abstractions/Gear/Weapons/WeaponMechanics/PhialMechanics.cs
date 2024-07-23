namespace SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

public class PhialMechanics : IWeaponMechanic
{
    public PhialMechanics(PhialType phialType, int phialPower)
    {
        PhialType = phialType;
        PhialPower = phialPower;
    }

    public PhialType PhialType { get; init; }
    public int PhialPower { get; init; }      // TODO: it looks like some of phial power entries are set to -1 when they should have an actual value
    public string TEMP_ToString()
    {
      return $"{nameof(PhialMechanics)}: Type: {PhialType}, Power: {PhialPower}";
    }
}