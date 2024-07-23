using SharedDataModels.Abstractions.Skills;

namespace SharedDataModels.Abstractions.Gear.Charms;

public class Charm
{
  public Charm(CharmId charmId, string name, int rarity, IEnumerable<GearSkill> skills)
  {
    CharmId = charmId;
    Name = name;
    Rarity = rarity;
    Skills = skills;
  }

  public CharmId CharmId { get; init; }
  public string Name { get; init; }
  public int Rarity { get; init; }
  public IEnumerable<GearSkill> Skills { get; init; }
}