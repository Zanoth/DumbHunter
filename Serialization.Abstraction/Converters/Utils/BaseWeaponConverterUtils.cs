using System.Collections;
using System.Text.Json;
using SharedDataModels.Abstractions.Gear.Weapons;
using SharedDataModels.Abstractions.Gear.Weapons.Stats;

namespace Serialization.Abstraction.Converters.Utils;

public static class BaseWeaponConverterUtils
{
  public static WeaponId ReadId(this JsonElement root)
  {
    var idJsonElement = root.GetProperty(nameof(IWeapon.WeaponId));
    var idStr = idJsonElement.GetString() ?? throw new ArgumentException($"Could not convert id to string: {idJsonElement.GetRawText}");
    var id = WeaponId.New(idStr);
    return id;
  }
  public static void WriteId(this Utf8JsonWriter writer, WeaponId id)
  {
    writer.WriteString(nameof(IWeapon.WeaponId), id.Id);
  }


  public static string ReadName(this JsonElement root)
  {
    var nameJsonElement = root.GetProperty(nameof(IWeapon.Name));
    var name = nameJsonElement.GetString() ?? throw new ArgumentException($"Could not convert name to string: {nameJsonElement.GetRawText}");
    return name;
  }
  public static void WriteName(this Utf8JsonWriter writer, string name)
  {
    writer.WriteString(nameof(IWeapon.Name), name);
  }


  public static int ReadRarity(this JsonElement root)
  {
    var rarityJsonElement = root.GetProperty(nameof(IWeapon.Rarity));
    var rarity = rarityJsonElement.GetInt32();
    return rarity;
  }
  public static void WriteRarity(this Utf8JsonWriter writer, int rarity)
  {
    writer.WriteNumber(nameof(IWeapon.Rarity), rarity);
  }


  public static int ReadAttack(this JsonElement root)
  {
    var attackJsonElement = root.GetProperty(nameof(IWeapon.Attack));
    var attack = attackJsonElement.GetInt32();
    return attack;
  }
  public static void WriteAttack(this Utf8JsonWriter writer, int attack)
  {
    writer.WriteNumber(nameof(IWeapon.Attack), attack);
  }


  public static int ReadAffinity(this JsonElement root)
  {
    var affinityJsonElement = root.GetProperty(nameof(IWeapon.Affinity));
    var affinity = affinityJsonElement.GetInt32();
    return affinity;
  }
  public static void WriteAffinity(this Utf8JsonWriter writer, int affinity)
  {
    writer.WriteNumber(nameof(IWeapon.Affinity), affinity);
  }


  public static int ReadDefense(this JsonElement root)
  {
    var defenseJsonElement = root.GetProperty(nameof(IWeapon.Defense));
    var defense = defenseJsonElement.GetInt32();
    return defense;
  }
  public static void WriteDefense(this Utf8JsonWriter writer, int defense)
  {
    writer.WriteNumber(nameof(IWeapon.Defense), defense);
  }


  public static bool ReadElementHidden(this JsonElement root)
  {
    var elementHiddenJsonElement = root.GetProperty(nameof(IWeapon.ElementHidden));
    var elementHidden = elementHiddenJsonElement.GetBoolean();
    return elementHidden;
  }
  public static void WriteElementHidden(this Utf8JsonWriter writer, bool elementHidden)
  {
    writer.WriteBoolean(nameof(IWeapon.ElementHidden), elementHidden);
  }


  public static Elderseal ReadElderseal(this JsonElement root)
  {
    var eldersealJsonElement = root.GetProperty(nameof(IWeapon.Elderseal));
    var eldersealStr = eldersealJsonElement.GetString();
    var elderseal = (Elderseal)Enum.Parse(typeof(Elderseal), eldersealStr);
    return elderseal;
  }
  public static void WriteElderseal(this Utf8JsonWriter writer, Elderseal elderseal)
  {
    writer.WriteString(nameof(IWeapon.Elderseal), elderseal.ToString());
  }


  public static IEnumerable<ElementStat> ReadElementStats(this JsonElement root)
  {
    var elementalStats = new List<ElementStat>();
    var elementsArray = root.GetProperty(nameof(IWeapon.ElementalStats)).EnumerateArray();
    while (elementsArray.MoveNext())
    {
      var elementJson = elementsArray.Current;

      var elementTypeJson = elementJson.GetProperty(nameof(ElementStat.ElementType));
      var elementType = (ElementType)Enum.Parse(typeof(ElementType), elementTypeJson.GetString());

      var elementDamageJson = elementJson.GetProperty(nameof(ElementStat.ElementAttack));
      var elementDamage = elementDamageJson.GetInt32();

      var elementStat = new ElementStat(elementType, elementDamage);
      elementalStats.Add(elementStat);
    }

    return elementalStats;
  }
  public static void WriteElementStats(this Utf8JsonWriter writer, IEnumerable<ElementStat> elementalStats)
  {
    writer.WriteStartArray(nameof(IWeapon.ElementalStats));
    foreach (var elementStat in elementalStats)
    {
      writer.WriteStartObject();

      writer.WriteString(nameof(ElementStat.ElementType), elementStat.ElementType.ToString());
      writer.WriteNumber(nameof(ElementStat.ElementAttack), elementStat.ElementAttack);

      writer.WriteEndObject();
    }
    writer.WriteEndArray();
  }


  public static IEnumerable<DecorationSlot> ReadDecorationSlots(this JsonElement root)
  {
    var decoSlots = new List<DecorationSlot>();
    var decoSlotArray = root.GetProperty(nameof(IWeapon.DecorationSlots)).EnumerateArray();
    while (decoSlotArray.MoveNext())
    {
      var decoSlotJson = decoSlotArray.Current;

      var slotLevel = decoSlotJson.GetProperty(nameof(DecorationSlot.SlotLevel)).GetInt32();

      var decoSlot = new DecorationSlot(slotLevel, DecorationId.Empty());
      decoSlots.Add(decoSlot);
    }

    return decoSlots;
  }
  public static void WriteDecorationSlots(this Utf8JsonWriter writer, IEnumerable<DecorationSlot> decorationSlots)
  {
    writer.WriteStartArray(nameof(IWeapon.DecorationSlots));
    foreach (var decoSlot in decorationSlots)
    {
      writer.WriteStartObject();

      writer.WriteNumber(nameof(DecorationSlot.SlotLevel), decoSlot.SlotLevel);
      writer.WriteString(nameof(DecorationSlot.AssignedDecorationId), decoSlot.AssignedDecorationId.Id);

      writer.WriteEndObject();
    }
    writer.WriteEndArray();
  }


  public static IEnumerable<GearSkill> ReadSkills(this JsonElement root)
  {
    // TODO: Fix this!
    //var skillJson = root.GetProperty(nameof(IWeapon.Skills));

    var dummy_skillLevels = new List<SkillLevel>();
    var dummy_skill = new Skill(SkillId.Empty(), "Skill Name", System.Drawing.Color.Red, 0, SkillId.Empty(), dummy_skillLevels);
    var dummy_level = 1;

    var gearSkill = new GearSkill(dummy_skill, dummy_level);
    return new List<GearSkill> { gearSkill };
  }

  public static void WriteSkills(this Utf8JsonWriter writer, IEnumerable<GearSkill> skills)
  {
    writer.WriteStartArray(nameof(IWeapon.Skills));
    foreach (var gearSkill in skills)
    {
      writer.WriteString(nameof(Skill.SkillId), gearSkill.Skill.SkillId.Id);
      writer.WriteNumber(nameof(GearSkill.SkillLevel), gearSkill.SkillLevel);
    }
    writer.WriteEndArray();
  }

  public static (WeaponId id,
    string name,
    int rarity,
    int attack, 
    int affinity, 
    int defense, 
    bool elementHidden,
    IEnumerable<ElementStat> elementalStats,
    Elderseal elderseal, 
    IEnumerable<DecorationSlot> decorationSlots,
    IEnumerable<GearSkill> skills) ReadBaseInfo(this JsonElement root)
  {
    var id = root.ReadId();
    var name = root.ReadName();
    var rarity = root.ReadRarity();

    var attack = root.ReadAttack();
    var affinity = root.ReadAffinity();
    var defense = root.ReadDefense();
    var elementHidden = root.ReadElementHidden();

    var elementalStats = root.ReadElementStats();
    var elderseal = root.ReadElderseal();

    var decorationSlots = root.ReadDecorationSlots();
    var skills = root.ReadSkills();

    return (id, name, rarity, attack, affinity, defense, elementHidden, elementalStats, elderseal, decorationSlots, skills);
  }
  public static void WriteBaseInfo(this Utf8JsonWriter writer, IWeapon weapon)
  {
    writer.WriteId(weapon.WeaponId);
    writer.WriteName(weapon.Name);
    writer.WriteRarity(weapon.Rarity);

    writer.WriteAttack(weapon.Attack);
    writer.WriteAffinity(weapon.Affinity);
    writer.WriteDefense(weapon.Defense);
    writer.WriteElementHidden(weapon.ElementHidden);

    writer.WriteElementStats(weapon.ElementalStats);
    writer.WriteElderseal(weapon.Elderseal);

    writer.WriteDecorationSlots(weapon.DecorationSlots);
    writer.WriteSkills(weapon.Skills);
  }
}