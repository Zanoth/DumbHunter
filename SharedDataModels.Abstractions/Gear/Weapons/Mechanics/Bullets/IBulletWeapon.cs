namespace SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Bullets;

public interface IBulletWeapon : IWeapon
{
    public BulletMechanic BulletMechanic { get; }
}