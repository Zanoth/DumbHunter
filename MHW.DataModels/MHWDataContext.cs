using MHW.DataModels.Decorations;
using MHW.DataModels.Gear.Charms;
using MHW.DataModels.Gear.Weapons;
using MHW.DataModels.Items;
using MHW.DataModels.Locations;
using MHW.DataModels.Quests;
using MHW.DataModels.Skills;
using Prism.Ioc;
using SharedDataModels.Abstractions;
using SharedDataModels.Abstractions.Gear.Charms;
using SharedDataModels.Abstractions.Gear.Weapons;
using SharedDataModels.Abstractions.Locations;
using SharedDataModels.Abstractions.Quests;

namespace MHW.DataModels;

public class MhwDataContext : IServiceRegistrator
{
  public void RegisterServices(IContainerRegistry container)
  {
    container.RegisterSingleton(typeof(IRepositoryFactory<SkillId, Skill>), typeof(RepositoryFactory<SkillId, Skill>));
    container.RegisterSingleton(typeof(IRepository<ArmorId, Armor>), typeof(ArmorRepository));
    container.RegisterSingleton(typeof(IRepository<WeaponId, IWeapon>), typeof(WeaponRepository));
    container.RegisterSingleton(typeof(IRepository<CharmId, Charm>), typeof(CharmRepository));
    container.RegisterSingleton(typeof(IRepository<SkillId, Skill>), typeof(SkillRepository));
    container.RegisterSingleton(typeof(IRepository<GearRecipeId, GearRecipe>), typeof(GearRecipeRepository));
    container.RegisterSingleton(typeof(IRepository<DecorationId, Decoration>), typeof(DecorationRepository));
    container.RegisterSingleton(typeof(IRepository<ItemId, Item>), typeof(ItemRepository));
    container.RegisterSingleton(typeof(IRepository<MonsterId, Monster>), typeof(MonsterRepository));
    container.RegisterSingleton(typeof(IRepository<QuestId, Quest>), typeof(QuestRepository));
    container.RegisterSingleton(typeof(IRepository<LocationId, Location>), typeof(LocationRepository));
  }
}
