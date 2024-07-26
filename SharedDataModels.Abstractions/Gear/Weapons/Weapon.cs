using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;
using SharedDataModels.Abstractions.Skills;

namespace SharedDataModels.Abstractions.Gear.Weapons;

public class Weapon
{
  public Weapon(WeaponId weaponId,
    string name,
    int rarity,
    WeaponType weaponType,
    int attack,
    int affinity,
    int defense,
    bool elementHidden,
    IEnumerable<ElementStat> elementalStats,
    Elderseal elderseal,
    IEnumerable<DecorationSlot> decoSlots,
    IEnumerable<GearSkill> skills,
    IWeaponMechanic? weaponMechanics,
    Sharpness? sharpness)
  {
    WeaponId = weaponId;
    Name = name;
    Rarity = rarity;
    WeaponType = weaponType;
    Attack = attack;
    Affinity = affinity;
    Defense = defense;
    ElementHidden = elementHidden;
    ElementalStats = elementalStats;
    Elderseal = elderseal;
    DecoSlots = decoSlots;
    Skills = skills;
    WeaponMechanics = weaponMechanics;
    Sharpness = sharpness;
  }

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

  public IEnumerable<DecorationSlot> DecoSlots { get; }

  public IEnumerable<GearSkill> Skills { get; }

  public IWeaponMechanic WeaponMechanics { get; }

  public Sharpness Sharpness { get; }
}