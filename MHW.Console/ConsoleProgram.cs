using FoundationExtensions;

namespace MHW.Console;

using Microsoft.Extensions.DependencyInjection;
using System;

public static class ConsoleProgram
{
  public static IServiceCollection ServiceCollection = new ServiceCollection();
  private static IServiceProvider? _serviceProvider;

  private static readonly Dictionary<int, (string, Func<Task>)> Functions = new Dictionary<int, (string, Func<Task>)>
  {
    //{ 0, ("[0] View Armor", async () => await ViewArmors())},
    { 1, ("[1] View Weapons",async () => await ViewWeapons())},
    //{ 2, ("[2] View Charms",async () => await ViewCharms())},
    //{ 3, ("[3] View Recipes", async () => await ViewRecipes())},
    ////{ 4, ("[4] View Inventory", async () => await ViewInventory())},
    ////{ 5, ("[5] View Wishlist", async () => await ViewWishlist())},
    //{ 6, ("[6] View Monsters", async () => await ViewMonsters())},
    //{ 7, ("[7] View Quests",async () => await ViewQuests())},
  };

  public static void Main(string[] args)
  {
    OutputUtilities.CreateOutputSpace();
    Console.WriteLine("Hello, World!");

    _serviceProvider = ServiceCollection.BuildServiceProvider();

    ChoseFunction();
  }

  private static async Task ChoseFunction()
  {
    Console.WriteLine("What would you like to test? - please input the value next to the option you'd like");
    foreach (var option in Functions)
    {
      Console.WriteLine(option.Value.Item1);
    }
    var userInput = Console.ReadLine();
    if (int.TryParse(userInput, out var choice) && Functions.TryGetValue(choice, out var test))
    {
      OutputUtilities.CreateOutputSpace();
      test.Item2.Invoke();
    }
    else
    {
      Console.WriteLine("Invalid input, please try again.");
      await ChoseFunction();
    }

    OutputUtilities.CreateOutputSpace();
    Console.WriteLine("Is there anything else you'd like to test? (y/n)");
    var continueTesting = Console.ReadLine()?.ToLower();

    while (continueTesting != "y" && continueTesting != "n")
    {
      Console.WriteLine("Invalid input, please try again.");
      continueTesting = Console.ReadLine()?.ToLower();
    }
    switch (continueTesting)
    {
      case "y":
        await ChoseFunction();
        break;
      case "n":
        Console.WriteLine("Goodbye!");
        break;
    }
  }

  private static async Task ViewWeapons()
  {
    var weaponRepository = _serviceProvider.GetRequiredService<IRepository<WeaponId, Weapon>>();
    var skillRepository = _serviceProvider.GetRequiredService<IRepository<SkillId, Skill>>();
    var weaponManager = new WeaponManager(weaponRepository, skillRepository);
    weaponManager.GreetUser();

  }
  private static async Task ViewArmors()
  {
    var armorRepository = _serviceProvider.GetRequiredService<IRepository<ArmorId, Armor>>();
    var skillRepository = _serviceProvider.GetRequiredService<IRepository<SkillId, Skill>>();

    var armors = await armorRepository.GetAllAsync();
    foreach (var armor in armors)
    {
      Console.WriteLine($"Armor: {armor.Name}");
      Console.WriteLine($"- Skills");
      foreach (var gearSkill in armor.Skills)
      {
        skillRepository.GetAsync(gearSkill.Skill.SkillId).ContinueWith(task =>
        {
          var skill = task.Result;
          var skillLevelDescription = skill.Levels.FirstOrDefault(l => l.Level == gearSkill.SkillLevel)?.Description;

          Console.WriteLine($"-- {skill.Name}, level {gearSkill.SkillLevel}: {skillLevelDescription}");
        });

      }
      Console.WriteLine($"------");
    }
  }
  private static async Task ViewCharms()
  {
    var charmRepository = _serviceProvider.GetRequiredService<IRepository<CharmId, Charm>>();
    var skillRepository = _serviceProvider.GetRequiredService<IRepository<SkillId, Skill>>();

    var charms = await charmRepository.GetAllAsync();
    foreach (var charm in charms)
    {
      Console.WriteLine($"Charm: {charm.CharmId}, {charm.Name} ");
      Console.WriteLine($"- Skills");
      foreach (var gearSkill in charm.Skills)
      {
        skillRepository.GetAsync(gearSkill.Skill.SkillId).ContinueWith(task =>
        {
          var skill = task.Result;
          var skillLevelDescription = skill.Levels.FirstOrDefault(l => l.Level == gearSkill.SkillLevel)?.Description;

          Console.WriteLine($"-- {skill.SkillId}, {skill.Name}, level {gearSkill.SkillLevel}: {skillLevelDescription}");
        });

      }
      Console.WriteLine($"------");
    }
  }
  private static async Task ViewRecipes()
  {
    var recipeRepository = _serviceProvider.GetRequiredService<IRepository<GearRecipeId, GearRecipe>>();
    var itemRepository = _serviceProvider.GetRequiredService<IRepository<ItemId, Item>>();
    var charmRepository = _serviceProvider.GetRequiredService<IRepository<CharmId, Charm>>();
    var armorRepository = _serviceProvider.GetRequiredService<IRepository<ArmorId, Armor>>();
    var weaponRepository = _serviceProvider.GetRequiredService<IRepository<WeaponId, Weapon>>();

    var recipes = await recipeRepository.GetAllAsync();
    foreach (var recipe in recipes)
    {
      var itemAndQuantity = recipe.Items;
      var items = new List<(Item, int)>();
      foreach ((ItemId itemId, int quantity) in itemAndQuantity)
      {
        var item = await itemRepository.GetAsync(itemId);
        items.Add((item, quantity));
      }


      var gearId = recipe.AssociatedGearId;
      var gearName = string.Empty;
      if (gearId is ArmorId armorId)
      {
        var armor = await armorRepository.GetAsync(armorId);
        gearName = armor.Name;
      }
      else if (gearId is WeaponId weaponId)
      {
        var weapon = await weaponRepository.GetAsync(weaponId);
        gearName = weapon.Name;
      }
      else if (gearId is CharmId charmId)
      {
        var charm = await charmRepository.GetAsync(charmId);
        gearName = charm.Name;
      }

      Console.WriteLine($"Recipe for {gearName}");
      foreach (var (item, quantity) in items)
      {
        Console.WriteLine($"- {item.Name} x{quantity}");
      }
    }
  }
  private static async Task ViewMonsters()
  {
    var monsterRepository = _serviceProvider.GetRequiredService<IRepository<MonsterId, Monster>>();
    var itemRepository = _serviceProvider.GetRequiredService<IRepository<ItemId, Item>>();

    var monsters = await monsterRepository.GetAllAsync();
    foreach (var monster in monsters)
    {
      Console.WriteLine($"Monster: {monster.MonsterId}: {monster.Name}");
      Console.WriteLine($"- Drops");
      foreach (var drop in monster.Drops)
      {
        var item = await itemRepository.GetAsync(drop.LootDetails.ItemId);
        Console.WriteLine($"-- item: {item.Name} - rank: {drop.QuestRank} - condition: {drop.DropCondition} - stack: {drop.LootDetails.Stack} - percentage: {drop.LootDetails.Percentage}");
      }
      Console.WriteLine($"------");
    }
  }
  private static async Task ViewQuests()
  {
    var questRepository = _serviceProvider.GetRequiredService<IRepository<QuestId, Quest>>();
    var monsterRepository = _serviceProvider.GetRequiredService<IRepository<MonsterId, Monster>>();

    var quests = await questRepository.GetAllAsync();
    foreach (var quest in quests)
    {
      Console.WriteLine($"quest: {quest.QuestId}: {quest.Name}");
      Console.WriteLine($"- Category: {quest.Category}");
      Console.WriteLine($"- Rank: {quest.Rank}");
      Console.WriteLine($"- Stars: {quest.Stars}");
      Console.WriteLine($"- Location: {quest.LocationId}");
      Console.WriteLine($"- Monsters");
      foreach (var (monsterId, quantity) in quest.Monsters)
      {
        var monster = await monsterRepository.GetAsync((MonsterId)monsterId);

        Console.WriteLine($"-- monster: {monster.Name} - quantity: {quantity}");
      }
      Console.WriteLine($"- Objective");
      foreach (var objective in quest.Objectives)
      {
        Console.WriteLine($"-- type: {objective.QuestType}");
        foreach (var requirement in objective.Requirements)
        {
          Console.WriteLine($"--- Id: {requirement.Id}, requiredCount: {requirement.Count}");
        }
      }

      Console.WriteLine($"- Rewards:");
      Console.WriteLine($"-- Zenny: {quest.Rewards.Zenny}");
      foreach (var lootDetails in quest.Rewards.Items)
      {
        var itemRepository = _serviceProvider.GetRequiredService<IRepository<ItemId, Item>>();
        var item = await itemRepository.GetAsync(lootDetails.ItemId);
        Console.WriteLine($"-- item: {item.Name} - quantity: {lootDetails.Stack} - percentage: {lootDetails.Percentage}");
      }

      Console.WriteLine($"------");
    }
  }
}