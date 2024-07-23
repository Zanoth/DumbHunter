using SharedDataModels.Abstractions;
using SharedDataModels.Abstractions.Gear.Armors;
using SharedDataModels.Abstractions.Gear.Charms;
using SharedDataModels.Abstractions.Gear.Weapons;

namespace MHW.Wpf.ViewModels;

public class CraftableGearViewModel
{
  private readonly IRepository<ArmorId, Armor> _armorRepository;
  private readonly IRepository<WeaponId, Weapon> _weaponRepository;
  private readonly IRepository<CharmId, Charm> _charmRepository;

  public CraftableGearViewModel(
    IRepository<ArmorId, Armor> armorRepository,
    IRepository<WeaponId, Weapon> weaponRepository,
    IRepository<CharmId, Charm> charmRepository)
  {
    _armorRepository = armorRepository;
    _weaponRepository = weaponRepository;
    _charmRepository = charmRepository;
  }

    public IEnumerable<Armor> Armors => _armorRepository.GetAll();
}
