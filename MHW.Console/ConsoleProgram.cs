namespace MHW.Console;

using Microsoft.Extensions.DependencyInjection;
using System;

public static class ConsoleProgram
{
  public static IServiceCollection ServiceCollection = new ServiceCollection();

  public static void Main(string[] args)
  {
    CreateOutputSpace();
    Console.WriteLine("Hello, World!");

    var serviceProvider = ServiceCollection.BuildServiceProvider();

    ChoseWhatToTest(serviceProvider);
  }

  private static async Task ChoseWhatToTest(IServiceProvider serviceProvider)
  {
    var testOptions = new Dictionary<int, (string, Func<Task>)>
    {
      { 0, ("[0] Armor", async () => await TestArmors(serviceProvider))},
      { 1, ("[1] Monster", async () => await TestMonsters(serviceProvider))},
      { 2, ("[2] Recipes", async () => await TestRecipes(serviceProvider))},
      { 3, ("[3] Weapons",async () => await TestWeapons(serviceProvider))},
      { 4, ("[4] Charms",async () => await TestCharms(serviceProvider))},
      { 5, ("[5] Quests",async () => await TestQuests(serviceProvider))},
    };

    Console.WriteLine("What would you like to test? - please input the value next to the option you'd like");
    foreach (var option in testOptions)
    {
      Console.WriteLine(option.Value.Item1);
    }
    var userInput = Console.ReadLine();
    if (int.TryParse(userInput, out var choice) && testOptions.TryGetValue(choice, out var test))
    {
      CreateOutputSpace();
      test.Item2.Invoke();
    }
    else
    {
      Console.WriteLine("Invalid input, please try again.");
      await ChoseWhatToTest(serviceProvider);
    }

    CreateOutputSpace();
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
        await ChoseWhatToTest(serviceProvider);
        break;
      case "n":
        Console.WriteLine("Goodbye!");
        break;
    }
  }

  private static void CreateOutputSpace()
  {
    Console.WriteLine("-------------------");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
  }

  private static async Task TestWeapons(IServiceProvider serviceProvider)
  {
    var weaponRepository = serviceProvider.GetRequiredService<IRepository<WeaponId, Weapon>>();
    var skillRepository = serviceProvider.GetRequiredService<IRepository<SkillId, Skill>>();

    var weapons = await weaponRepository.GetAllAsync();
    foreach (var weapon in weapons)
    {
      Console.WriteLine($"Weapon: {weapon.WeaponId}, {weapon.Name}");
      Console.WriteLine($"- Type: {weapon.WeaponType}");
      Console.WriteLine($"- Rarity: {weapon.Rarity}");
      Console.WriteLine($"- Attack-stuff");
      Console.WriteLine($"-- Attack: {weapon.Attack}");
      Console.WriteLine($"-- Affinity: {weapon.Affinity}");
      foreach (var element in weapon.ElementalStats)
      {
        Console.WriteLine($"-- Element: {element.ElementType} - {element.ElementAttack}");
      }
      Console.WriteLine($"- Sharpness");
      foreach (var sharpnessSection in weapon.Sharpness.Sections)
      {
        Console.WriteLine($"-- {sharpnessSection.Color} - {sharpnessSection.Value}");
      }
      Console.WriteLine($"- Slots");
      foreach (var slot in weapon.DecoSlots)
      {
        Console.WriteLine($"-- {slot.SlotLevel}");
      }
      Console.WriteLine($"- Skills");
      foreach (var gearSkill in weapon.Skills)
      {
        skillRepository.GetAsync(gearSkill.Skill.SkillId).ContinueWith(task =>
        {
          var skill = task.Result;
          var skillLevelDescription = skill.Levels.FirstOrDefault(l => l.Level == gearSkill.SkillLevel)?.Description;

          Console.WriteLine($"-- {skill.Name}, level {gearSkill.SkillLevel}: {skillLevelDescription}");
        });

      }
      Console.WriteLine($"Elderseal: {weapon.Elderseal}");
      Console.WriteLine($"WeaponMechanics: {weapon.WeaponMechanics.TEMP_ToString()}");

      CreateOutputSpace();
    }
  }
  private static async Task TestArmors(IServiceProvider serviceProvider)
  {
    var armorRepository = serviceProvider.GetRequiredService<IRepository<ArmorId, Armor>>();
    var skillRepository = serviceProvider.GetRequiredService<IRepository<SkillId, Skill>>();

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
  private static async Task TestCharms(IServiceProvider serviceProvider)
  {
    var charmRepository = serviceProvider.GetRequiredService<IRepository<CharmId, Charm>>();
    var skillRepository = serviceProvider.GetRequiredService<IRepository<SkillId, Skill>>();

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
  private static async Task TestRecipes(IServiceProvider serviceProvider)
  {
    var recipeRepository = serviceProvider.GetRequiredService<IRepository<GearRecipeId, GearRecipe>>();
    var itemRepository = serviceProvider.GetRequiredService<IRepository<ItemId, Item>>();
    var charmRepository = serviceProvider.GetRequiredService<IRepository<CharmId, Charm>>();
    var armorRepository = serviceProvider.GetRequiredService<IRepository<ArmorId, Armor>>();
    var weaponRepository = serviceProvider.GetRequiredService<IRepository<WeaponId, Weapon>>();

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
  private static async Task TestMonsters(IServiceProvider serviceProvider)
  {
    var monsterRepository = serviceProvider.GetRequiredService<IRepository<MonsterId, Monster>>();
    var itemRepository = serviceProvider.GetRequiredService<IRepository<ItemId, Item>>();

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
  private static async Task TestQuests(IServiceProvider serviceProvider)
  {
    var questRepository = serviceProvider.GetRequiredService<IRepository<QuestId, Quest>>();
    var monsterRepository = serviceProvider.GetRequiredService<IRepository<MonsterId, Monster>>();

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
        var itemRepository = serviceProvider.GetRequiredService<IRepository<ItemId, Item>>();
        var item = await itemRepository.GetAsync(lootDetails.ItemId);
        Console.WriteLine($"-- item: {item.Name} - quantity: {lootDetails.Stack} - percentage: {lootDetails.Percentage}");
      }

      Console.WriteLine($"------");
    }
  }
}