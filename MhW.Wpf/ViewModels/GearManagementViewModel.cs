using SharedDataModels.Abstractions;
using SharedDataModels.Abstractions.Gear.Armors;
using SharedDataModels.Abstractions.Gear.Weapons;
using SharedDataModels.Abstractions.Gear.Weapons.Bows;
using SharedDataModels.Abstractions.Gear.Weapons.ChargeBlades;
using SharedDataModels.Abstractions.Gear.Weapons.DualBlades;
using SharedDataModels.Abstractions.Gear.Weapons.GreatSwords;
using SharedDataModels.Abstractions.Gear.Weapons.Gunlances;
using SharedDataModels.Abstractions.Gear.Weapons.Hammers;
using SharedDataModels.Abstractions.Gear.Weapons.HeavyBowGuns;
using SharedDataModels.Abstractions.Gear.Weapons.HuntingHorns;
using SharedDataModels.Abstractions.Gear.Weapons.insectGlaives;
using SharedDataModels.Abstractions.Gear.Weapons.Lances;
using SharedDataModels.Abstractions.Gear.Weapons.LightBowGuns;
using SharedDataModels.Abstractions.Gear.Weapons.LongSwords;
using SharedDataModels.Abstractions.Gear.Weapons.SwitchAxes;
using SharedDataModels.Abstractions.Gear.Weapons.SwordAndShields;

namespace MHW.Wpf.ViewModels;

public class GearManagementViewModel
{
  private readonly IRepository<WeaponId, IWeapon> _weaponRepository;
  private readonly IRepository<ArmorId, Armor> _armorRepository;

  public GearManagementViewModel(IRepository<WeaponId, IWeapon> weaponRepository, IRepository<ArmorId, Armor> armorRepository)
  {
    _weaponRepository = weaponRepository;
    _armorRepository = armorRepository;

    var weapons = _weaponRepository.GetAll();
    Bows = weapons.OfType<IBow>();
    ChargeBlades = weapons.OfType<IChargeBlade>();
    DualBlades = weapons.OfType<IDualBlades>();
    GreatSwords = weapons.OfType<IGreatSword>();
    Gunlances = weapons.OfType<IGunlance>();
    Hammers = weapons.OfType<IHammer>();
    HeavyBowGuns = weapons.OfType<IHeavyBowGun>();
    HuntingHorns = weapons.OfType<IHuntingHorn>();
    InsectGlaives = weapons.OfType<IInsectGlaive>();
    Lances = weapons.OfType<ILance>();
    LightBowGuns = weapons.OfType<ILightBowGun>();
    Longswords = weapons.OfType<ILongsword>();
    SwitchAxes = weapons.OfType<ISwitchAxe>();
    SwordAndShields = weapons.OfType<ISwordAndShield>();

    Armors = _armorRepository.GetAll();
  }

  public IEnumerable<IBow> Bows { get; }
  public IEnumerable<IChargeBlade> ChargeBlades { get; }
  public IEnumerable<IDualBlades> DualBlades { get; }
  public IEnumerable<IGreatSword> GreatSwords { get; }
  public IEnumerable<IGunlance> Gunlances { get; }
  public IEnumerable<IHammer> Hammers { get; }
  public IEnumerable<IHeavyBowGun> HeavyBowGuns { get; }
  public IEnumerable<IHuntingHorn> HuntingHorns { get; }  
  public IEnumerable<IInsectGlaive> InsectGlaives { get; }
  public IEnumerable <ILance> Lances { get; }
  public IEnumerable<ILightBowGun> LightBowGuns { get; }
  public IEnumerable<ILongsword> Longswords { get; }
  public IEnumerable<ISwitchAxe> SwitchAxes { get; }
  public IEnumerable<ISwordAndShield> SwordAndShields { get; }







  public IEnumerable<Armor> Armors { get; }
}