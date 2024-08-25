namespace SharedDataModels.Abstractions.Monsters;

public class Monster
{
  public Monster(MonsterId monsterId, string name, MonsterEcology ecology, MonsterSize size, string iconName, TrapInteractions trapInteractions, IEnumerable<Drop> drops, IEnumerable<FormWeakness> formWeaknesses)
  {
    MonsterId = monsterId;
    Name = name;
    Ecology = ecology;
    Size = size;
    IconName = iconName;
    TrapInteractions = trapInteractions;
    Drops = drops;
    FormWeaknesses = formWeaknesses;
  }

  public MonsterId MonsterId { get; }
  public string Name { get; }
  public MonsterEcology Ecology { get; }
  public MonsterSize Size { get; }
  public string IconName { get; }
  public TrapInteractions TrapInteractions { get; }
  public IEnumerable<Drop> Drops { get; }
  public IEnumerable<FormWeakness> FormWeaknesses { get; }
}