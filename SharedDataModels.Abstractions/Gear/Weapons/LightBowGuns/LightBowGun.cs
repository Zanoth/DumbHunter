﻿using SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Bullets;
using SharedDataModels.Abstractions.Gear.Weapons.Stats;
using SharedDataModels.Abstractions.Skills;

namespace SharedDataModels.Abstractions.Gear.Weapons.LightBowGuns;

public class LightBowGun : ILightBowGun
{
    public LightBowGun(WeaponId weaponId,
      string name,
      int rarity,
      int attack,
      int affinity,
      int defense,
      bool elementHidden,
      IEnumerable<ElementStat> elementalStats,
      Elderseal elderseal,
      IEnumerable<DecorationSlot> decorationSlots,
      IEnumerable<GearSkill> skills,
      BulletMechanic bulletMechanic)
    {
        WeaponId = weaponId;
        Name = name;
        Rarity = rarity;
        Attack = attack;
        Affinity = affinity;
        Defense = defense;
        ElementHidden = elementHidden;
        ElementalStats = elementalStats;
        Elderseal = elderseal;
        DecorationSlots = decorationSlots;
        Skills = skills;
        BulletMechanic = bulletMechanic;
    }


    public WeaponId WeaponId { get; }
    public string Name { get; }
    public int Rarity { get; }
    public WeaponType WeaponType { get; } = WeaponType.LightBowGun;
    public int Attack { get; }
    public int Affinity { get; }
    public int Defense { get; }
    public bool ElementHidden { get; }
    public IEnumerable<ElementStat> ElementalStats { get; }
    public Elderseal Elderseal { get; }
    public IEnumerable<DecorationSlot> DecorationSlots { get; }
    public IEnumerable<GearSkill> Skills { get; }
    public BulletMechanic BulletMechanic { get; }
}