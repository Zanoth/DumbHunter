using FoundationExtensions;
using SharedDataModels.Abstractions.Gear.Weapons;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace MHW.Console;

public class WeaponManager
{
  private readonly IRepository<WeaponId, Weapon> _weaponRepository;
  private readonly IRepository<SkillId, Skill> _skillRepository;
  private readonly MultiKeyDictionary<string, MethodInfo> _userFunctions = new();

  public WeaponManager(IRepository<WeaponId, Weapon> weaponRepository, IRepository<SkillId, Skill> skillRepository)
  {
    _weaponRepository = weaponRepository;
    _skillRepository = skillRepository;

    _userFunctions = BuildUserFunctionCollection();
  }

  private MultiKeyDictionary<string, MethodInfo> BuildUserFunctionCollection()
  {
    var functions = new MultiKeyDictionary<string, MethodInfo>();

    var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    foreach (var method in methods)
    {
      var attribute = method.GetCustomAttribute<UserCommand>();
      if (attribute == null) continue;

      var shortForm = attribute.ShortFormActivationInput.ToLower();
      var longForm = attribute.LongFormActivationInput.ToLower();
      functions.Add(shortForm, longForm, method);
    }
    return functions;
  }

  [UserCommand("0", "WeaponList", "List all weapons")]
  public async Task ListAllWeapons()
  {
    var weapons = await _weaponRepository.GetAllAsync();
    foreach (var weapon in weapons)
      OutputUtilities.WriteLine($"Weapon: {weapon.WeaponId.Id}, {weapon.Name}, {weapon.WeaponType}");
  }

  [UserCommand("1", "SpecsList", "Full specs list of all weapons")]
  public async Task ListSpecsOfAllWeapons()
  {
    var weapons = await _weaponRepository.GetAllAsync();
    foreach (var weapon in weapons)
      WriteWeaponSpecs(weapon);
  }

  [UserCommand("2", "WeaponSpecs", "Full specs list of a specific weapons")]
  public async Task ListSpecOfWeapon()
  {
    OutputUtilities.WriteLine("Please enter the weapon ID of the weapon you'd like to view.");
    var continueLoop = true;
    while (continueLoop)
    {
      var weaponIdStr = OutputUtilities.GetUserInput();
      var weaponId = new WeaponId(weaponIdStr);

      var weaponLocated = await _weaponRepository.TryGetAsync(weaponId, out var weapon);
      if (!weaponLocated)
      {
        OutputUtilities.WriteLine("Weapon not found.");
        OutputUtilities.ContinueLoop(continueLoop, "Would you like to view another weapon? (Y/N)");
      }
      else
      {
        WriteWeaponSpecs(weapon);
      }
    }
  }

  public async void GreetUser()
  {
    OutputUtilities.WriteLine($"-- This is the Weapons section. Here are the available commands --");

    var continueLoop = true;
    while (continueLoop)
    {
      await ListUserCommands();
      await HandleUserCommands();

      continueLoop = ContinueLoop(continueLoop, "Would you like to execute another command? (Y/N)");
    }
  }

  private static bool ContinueLoop(bool continueLoop, string LopQuery)
  {
    OutputUtilities.WriteLine(LopQuery);

    var userInput = "";
    while (userInput != "y")
    {
      userInput = OutputUtilities.GetUserInput().ToLower();
      if (userInput == "n")
      {
        continueLoop = false;
        break;
      }

      if (userInput != "y")
        OutputUtilities.WriteLine("Invalid input. Please try again.");
    }
    return continueLoop;
  }

  private async Task ListUserCommands()
  {
    foreach (var (input, method) in _userFunctions)
      await OutputUtilities.WriteLine($"- {input.ShortForm} or {input.LongForm} : {method.GetCustomAttribute<UserCommand>().Description}");
  }

  private async Task HandleUserCommands()
  {
    OutputUtilities.WriteLine($"Please enter the short or long form input of the command you'd like to execute.");
    var userInput = OutputUtilities.GetUserInput().ToLower();

    if (_userFunctions.TryGetValue(userInput, out MethodInfo method))
    {
      var result = method.Invoke(this, null);
      if (result is Task task)
      {
        await task;
      }
    }
    else
      OutputUtilities.WriteLine("Invalid command.");
  }
  private void WriteWeaponSpecs(Weapon weapon)
  {
    // Define column widths
    const int idWidth = 15;
    const int nameWidth = 20;
    const int typeWidth = 15;
    const int rarityWidth = 10;
    const int attackWidth = 10;
    const int affinityWidth = 10;
    const int elementWidth = 15;
    const int eldersealWidth = 10;
    const int sharpnessWidth = 15;
    const int slotWidth = 10;
    const int skillWidth = 30;
    const int weaponMechanicsWidth = 40;

    // force the enumeration, so it only happens once
    var elementalStats = weapon.ElementalStats.ToList();
    var sharpnessSections = weapon.Sharpness.Sections.ToList();
    var decorationSlots = weapon.DecoSlots.ToList();
    var weaponSkills = weapon.Skills.ToList();

    // Calculate the maximum number of lines needed
    var maxLines = Math.Max(
        Math.Max(elementalStats.Count, sharpnessSections.Count),
        Math.Max(decorationSlots.Count, weaponSkills.Count)
    );

    // Print header
    OutputUtilities.WriteLine(
        $"{OutputUtilities.PadRight("Weapon ID", idWidth)}" +
        $"{OutputUtilities.PadRight("Name", nameWidth)}" +
        $"{OutputUtilities.PadRight("Type", typeWidth)}" +
        $"{OutputUtilities.PadRight("Rarity", rarityWidth)}" +
        $"{OutputUtilities.PadRight("Attack", attackWidth)}" +
        $"{OutputUtilities.PadRight("Affinity", affinityWidth)}" +
        $"{OutputUtilities.PadRight("Elderseal", eldersealWidth)}" +
        $"{OutputUtilities.PadRight("Element", elementWidth)}" +
        $"{OutputUtilities.PadRight("Sharpness", sharpnessWidth)}" +
        $"{OutputUtilities.PadRight("Slots", slotWidth)}" +
        $"{OutputUtilities.PadRight("Skills", skillWidth)}" +
        $"{OutputUtilities.PadRight("Mechanics", weaponMechanicsWidth)}"
    );

    // Print weapon details line by line
    for (int i = 0; i < maxLines; i++)
    {
      var idStr = i == 0 ? weapon.WeaponId.Id : "";
      var nameStr = i == 0 ? weapon.Name : "";
      var typeStr = i == 0 ? weapon.WeaponType.ToString() : "";
      var rarityStr = i == 0 ? weapon.Rarity.ToString() : "";
      var attackStr = i == 0 ? weapon.Attack.ToString() : "";
      var affinityStr = i == 0 ? weapon.Affinity.ToString() : "";
      var eldersealStr = i == 0 ? weapon.Elderseal.ToString() : "";
      var elementalStatusStr = i < elementalStats.Count && elementalStats.Count > 0 ? $"{elementalStats[i].ElementType} - {elementalStats[i].ElementAttack}" : "";
      var sharpnessSectionsStr = i < sharpnessSections.Count && sharpnessSections.Count > 0 ? $"[{sharpnessSections[i].Color.Name}] - {sharpnessSections[i].Value}" : "";
      var decorationSlotsStr = i < decorationSlots.Count && decorationSlots.Count > 0 ? decorationSlots[i].SlotLevel.ToString() : "";
      var weaponSkillsStr = i < weaponSkills.Count && weaponSkills.Count > 0 ? $"{weaponSkills[i].Skill.Name} (Level {weaponSkills[i].SkillLevel})" : "";
      var mechanicsStr = i == 0 ? weapon.WeaponMechanics.TEMP_ToString() : "";

      OutputUtilities.WriteLine(
          $"{OutputUtilities.PadRight(idStr, idWidth)}" +
          $"{OutputUtilities.PadRight(nameStr, nameWidth)}" +
          $"{OutputUtilities.PadRight(typeStr, typeWidth)}" +
          $"{OutputUtilities.PadRight(rarityStr, rarityWidth)}" +
          $"{OutputUtilities.PadRight(attackStr, attackWidth)}" +
          $"{OutputUtilities.PadRight(affinityStr, affinityWidth)}" +
          $"{OutputUtilities.PadRight(eldersealStr, eldersealWidth)}" +
          $"{OutputUtilities.PadRight(elementalStatusStr, elementWidth)}" +
          $"{OutputUtilities.PadRight(sharpnessSectionsStr, sharpnessWidth)}" +
          $"{OutputUtilities.PadRight(decorationSlotsStr, slotWidth)}" +
          $"{OutputUtilities.PadRight(weaponSkillsStr, skillWidth)}" +
          $"{OutputUtilities.PadRight(mechanicsStr, weaponMechanicsWidth)}"
      );
    }

    OutputUtilities.CreateOutputSpace();
  }
}