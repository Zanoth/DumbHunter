namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Melodies;

public interface IMelodyWeapon : ISharpnessWeapon
{
    public MelodyMechanic MelodyMechanic { get; }
}