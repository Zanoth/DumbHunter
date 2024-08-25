using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace SharedDataModels.Abstractions.Gear.Weapons;

public interface ISharpnessWeapon : IWeapon
{
    public Sharpness Sharpness { get; }
}