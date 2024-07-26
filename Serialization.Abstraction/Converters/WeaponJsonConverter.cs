using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class WeaponJsonConverter : JsonConverter<Weapon>, IJsonConverter
{
  public override Weapon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = WeaponId.New(root.GetProperty(nameof(Weapon.WeaponId)).GetString());
    var name = root.GetProperty(nameof(Weapon.Name)).GetString();
    var rarity = root.GetProperty(nameof(Weapon.Rarity)).GetInt32();

    var weaponTypeJson = root.GetProperty(nameof(Weapon.WeaponType));
    var weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), weaponTypeJson.GetString());

    var attack = root.GetProperty(nameof(Weapon.Attack)).GetInt32();
    var affinity = root.GetProperty(nameof(Weapon.Affinity)).GetInt32();
    var defense = root.GetProperty(nameof(Weapon.Defense)).GetInt32();
    var elementHidden = root.GetProperty(nameof(Weapon.ElementHidden)).GetBoolean();

    var eldersealStr = root.GetProperty(nameof(Weapon.Elderseal)).GetString();
    var elderseal = (Elderseal)Enum.Parse(typeof(Elderseal), eldersealStr);

    var skills = new List<GearSkill>();
    var skillsArray = root.GetProperty(nameof(Weapon.Skills)).EnumerateArray();
    while (skillsArray.MoveNext())
    {
      var skillJson = skillsArray.Current.GetRawText();
      var gearSkill = JsonSerializer.Deserialize<GearSkill>(skillJson, options);
      skills.Add(gearSkill);
    }

    var weaponMechanicStats = ReadWeaponMechanic(weaponType, root);

    var sharpnessRawText = root.GetProperty(nameof(Weapon.Sharpness)).GetRawText();
    var sharpness = JsonSerializer.Deserialize<Sharpness>(sharpnessRawText, options);

    //TODO: Consider implementing a jsonConverter for elementalStats
    var elementalStats = ReadElementStats(root);

    var decoSlots = ReadDecorationSlots(root);

    var weapon = new Weapon(id, name, rarity, weaponType, attack, affinity, defense, elementHidden, elementalStats, elderseal, decoSlots, skills, weaponMechanicStats, sharpness);
    return weapon;
  }

  private static IWeaponMechanic ReadWeaponMechanic(WeaponType weaponType, JsonElement root)
  {
    IWeaponMechanic weaponMechanicStats = weaponType switch
    {
      WeaponType.Gunlance => JsonSerializer.Deserialize<ShellingMechanics>(root
        .GetProperty(nameof(Weapon.WeaponMechanics))
        .GetRawText()),

      WeaponType.SwitchAxe or WeaponType.ChargeBlade => JsonSerializer.Deserialize<PhialMechanics>(
        root.GetProperty(nameof(Weapon.WeaponMechanics))
          .GetRawText()),

      WeaponType.InsectGlaive => JsonSerializer.Deserialize<KinsectMechanics>(
        root.GetProperty(nameof(Weapon.WeaponMechanics))
          .GetRawText()),

      WeaponType.Bow or
        WeaponType.LightBowgun or
        WeaponType.HeavyBowgun => JsonSerializer.Deserialize<AmmoMechanics>(
          root.GetProperty(nameof(Weapon.WeaponMechanics))
            .GetRawText()),

      WeaponType.DualBlades or
        WeaponType.LongSword or
        WeaponType.GreatSword or
        WeaponType.Hammer or
        WeaponType.HuntingHorn or
        WeaponType.Lance or
        WeaponType.SwordAndShield or
        WeaponType.ChargeBlade or
        WeaponType.SwitchAxe => new NonMechanic(),

      _ => (IWeaponMechanic) new NotYetImplementedMechanic()
    } ?? new NonMechanic();
    return weaponMechanicStats;
  }

  private static List<ElementStat> ReadElementStats(JsonElement root)
  {
    var elementalStats = new List<ElementStat>();
    var elementsArray = root.GetProperty(nameof(Weapon.ElementalStats)).EnumerateArray();
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

  private static List<DecorationSlot> ReadDecorationSlots(JsonElement root)
  {
    var decoSlots = new List<DecorationSlot>();
    var decoSlotArray = root.GetProperty(nameof(Weapon.DecoSlots)).EnumerateArray();
    while (decoSlotArray.MoveNext())
    {
      var decoSlotJson = decoSlotArray.Current;

      var slotLevel = decoSlotJson.GetProperty(nameof(DecorationSlot.SlotLevel)).GetInt32();
      var assignedDecorationId = decoSlotJson.GetProperty(nameof(DecorationSlot.AssignedDecorationId)).GetString();

      var decoSlot = new DecorationSlot(slotLevel, DecorationId.New(assignedDecorationId));
      decoSlots.Add(decoSlot);
    }

    return decoSlots;
  }

  public override void Write(Utf8JsonWriter writer, Weapon weapon, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WriteString(nameof(Weapon.WeaponId), weapon.WeaponId.Id);
    writer.WriteString(nameof(Weapon.Name), weapon.Name);
    writer.WriteNumber(nameof(Weapon.Rarity), weapon.Rarity);
    writer.WriteString(nameof(Weapon.WeaponType), weapon.WeaponType.ToString());
    writer.WriteNumber(nameof(Weapon.Attack), weapon.Attack);
    writer.WriteNumber(nameof(Weapon.Affinity), weapon.Affinity);
    writer.WriteNumber(nameof(Weapon.Defense), weapon.Defense);
    writer.WriteBoolean(nameof(Weapon.ElementHidden), weapon.ElementHidden);

    WriteElementalStats(writer, weapon);

    writer.WriteString(nameof(Weapon.Elderseal), weapon.Elderseal.ToString());

    writer.WritePropertyName(nameof(Weapon.DecoSlots));
    writer.WriteRawValue(JsonSerializer.Serialize(weapon.DecoSlots, options));
    //writer.WriteString(nameof(Weapon.Skill), value.Skill.SkillId); //TODO: Add Skill back in

    //writer.WritePropertyName(nameof(Weapon.WeaponMechanicStats));
    //writer.WriteRawValue(JsonSerializer.Serialize(weapon.WeaponMechanicStats, options));

    writer.WritePropertyName(nameof(Weapon.Sharpness));
    writer.WriteRawValue(JsonSerializer.Serialize(weapon.Sharpness, options));

    writer.WriteEndObject();
  }

  private static void WriteElementalStats(Utf8JsonWriter writer, Weapon weapon)
  {
    writer.WriteStartArray(nameof(Weapon.ElementalStats));
    foreach (var elementStat in weapon.ElementalStats)
    {
      writer.WriteStartObject();

      writer.WriteString(nameof(ElementStat.ElementType), elementStat.ElementType.ToString());
      writer.WriteNumber(nameof(ElementStat.ElementAttack), elementStat.ElementAttack);

      writer.WriteEndObject();
    }
    writer.WriteEndArray();
  }
}