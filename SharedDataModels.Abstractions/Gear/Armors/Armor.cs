using SharedDataModels.Abstractions.Skills;

namespace SharedDataModels.Abstractions.Gear.Armors;

public class Armor
{
  public Armor(ArmorId armorId, string name, int rarity, ArmorType armorType, IEnumerable<GearSkill> skills, IEnumerable<DecorationSlot> decoSlots, DefenseStats defenseStats)
  {
    ArmorId = armorId;
    Name = name;
    Rarity = rarity;
    ArmorType = armorType;
    Skills = skills;
    DecoSlots = decoSlots;
    DefenseStats = defenseStats;
  }

  public ArmorId ArmorId { get; init; }
  public string Name { get; init; }
  public int Rarity { get; init; }
  public ArmorType ArmorType { get; init; }
  public IEnumerable<GearSkill> Skills { get; }
  public IEnumerable<DecorationSlot> DecoSlots { get; init; }
  public DefenseStats DefenseStats { get; }
}