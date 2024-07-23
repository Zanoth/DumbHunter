using MHW.DataModels.Decorations;
using MHW.DataModels.Gear.Charms;
using MHW.DataModels.Gear.Weapons;
using MHW.DataModels.Items;
using MHW.DataModels.Locations;
using MHW.DataModels.Quests;
using MHW.DataModels.Skills;
using Microsoft.Extensions.DependencyInjection;
using SharedDataModels.Abstractions;
using SharedDataModels.Abstractions.Gear.Charms;
using SharedDataModels.Abstractions.Locations;
using SharedDataModels.Abstractions.Quests;
using System;

namespace MHW.DataModels;

public class MhwDataContext : ContextBase, IServiceRegistrar
{
  public void RegisterServices(IServiceCollection services)
  {
    services.AddTransient<IRepositoryFactory<SkillId, Skill>, RepositoryFactory<SkillId, Skill>>();
   
    RegisterService(services, typeof(IRepository<ArmorId, Armor>), typeof(ArmorRepository));
    RegisterService(services, typeof(IRepository<WeaponId, Weapon>), typeof(WeaponRepository));
    RegisterService(services, typeof(IRepository<CharmId, Charm>), typeof(CharmRepository));
    RegisterService(services, typeof(IRepository<SkillId, Skill>), typeof(SkillRepository));
    RegisterService(services, typeof(IRepository<GearRecipeId, GearRecipe>), typeof(GearRecipeRepository));
    RegisterService(services, typeof(IRepository<DecorationId, Decoration>), typeof(DecorationRepository));
    RegisterService(services, typeof(IRepository<ItemId, Item>), typeof(ItemRepository));
    RegisterService(services, typeof(IRepository<MonsterId, Monster>), typeof(MonsterRepository));
    RegisterService(services, typeof(IRepository<QuestId, Quest>), typeof(QuestRepository));
    RegisterService(services, typeof(IRepository<LocationId, Location>), typeof(LocationRepository));
  }
}
