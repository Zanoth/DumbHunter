namespace SharedDataModels.Abstractions.Monsters;

public class Monster
{
  public Monster(MonsterId monsterId, string name, MonsterEcology ecology, MonsterSize size, TrapInteractions trapInteractions, IEnumerable<Drop> drops)
  {
    MonsterId = monsterId;
    Name = name;
    Ecology = ecology;
    Size = size;
    TrapInteractions = trapInteractions;
    Drops = drops;
  }


  public MonsterId MonsterId { get; init; }
  public string Name { get; init; }
  public MonsterEcology Ecology { get; init; }
  public MonsterSize Size { get; init; }
  public TrapInteractions TrapInteractions { get; init; }
  public IEnumerable<Drop> Drops { get; }

  //TODO: Missing Monster Weaknesses
}