using SharedDataModels.Abstractions;
using SharedDataModels.Abstractions.Monsters;

namespace MHW.Wpf.ViewModels;

public class MonsterListViewModel
{
  private readonly IRepository<MonsterId, Monster> _monsterRepository;

  public MonsterListViewModel(IRepository<MonsterId, Monster> monsterRepository)
  {
    _monsterRepository = monsterRepository;
    Monsters = _monsterRepository.GetAll();
  }

  public IEnumerable<Monster> Monsters { get; set; }
}