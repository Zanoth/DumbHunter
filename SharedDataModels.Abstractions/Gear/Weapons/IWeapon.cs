using SharedDataModels.Abstractions.Gear.Weapons.Stats;
using SharedDataModels.Abstractions.Skills;

namespace SharedDataModels.Abstractions.Gear.Weapons;

public interface IWeapon
{
    public WeaponId WeaponId { get; }
    public string Name { get; }
    public int Rarity { get; }
    public WeaponType WeaponType { get; }
    public int Attack { get; }
    public int Affinity { get; }
    public int Defense { get; }
    public bool ElementHidden { get; }
    public IEnumerable<ElementStat> ElementalStats { get; }
    public Elderseal Elderseal { get; }
    public IEnumerable<DecorationSlot> DecorationSlots { get; }
    public IEnumerable<GearSkill> Skills { get; }
}