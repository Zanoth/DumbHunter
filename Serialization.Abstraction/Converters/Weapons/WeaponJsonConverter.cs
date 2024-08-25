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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters.Weapons;

public class WeaponJsonConverter : JsonConverter<IWeapon>, IJsonConverter
{
  public override IWeapon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var weaponTypeJson = root.GetProperty(nameof(IWeapon.WeaponType));
    var weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), weaponTypeJson.GetString());

    var weapon = weaponType switch
    {
      WeaponType.GreatSword => root.Deserialize<GreatSword>(options),
      WeaponType.LongSword => root.Deserialize<LongSword>(options),
      WeaponType.SwordAndShield => root.Deserialize<SwordAndShield>(options),
      WeaponType.DualBlades => root.Deserialize<DualBlades>(options),
      WeaponType.Hammer => root.Deserialize<Hammer>(options),
      WeaponType.HuntingHorn => root.Deserialize<HuntingHorn>(options),
      WeaponType.Lance => root.Deserialize<Lance>(options),
      WeaponType.Gunlance => root.Deserialize<Gunlance>(options),
      WeaponType.SwitchAxe => root.Deserialize<SwitchAxe>(options),
      WeaponType.ChargeBlade => root.Deserialize<ChargeBlade>(options),
      WeaponType.InsectGlaive => root.Deserialize<InsectGlaive>(options),
      WeaponType.LightBowGun => root.Deserialize<LightBowGun>(options),
      WeaponType.HeavyBowGun => root.Deserialize<HeavyBowGun>(options),
      WeaponType.Bow => (IWeapon?)root.Deserialize<Bow>(options),
      _ => throw new ArgumentException()
    };

    return weapon ?? throw new JsonException();
  }

  public override void Write(Utf8JsonWriter writer, IWeapon weapon, JsonSerializerOptions options)
  {
    switch (weapon.WeaponType)
    {
      case WeaponType.GreatSword:
        var greatSword = (GreatSword)weapon;
        JsonSerializer.Serialize(writer, greatSword, options);
        break;
      case WeaponType.LongSword:
        var longsword = (LongSword)weapon;
        JsonSerializer.Serialize(writer, longsword, options);
        break;
      case WeaponType.SwordAndShield:
        var swordAndShield = (SwordAndShield)weapon;
        JsonSerializer.Serialize(writer, swordAndShield, options);
        break;
      case WeaponType.DualBlades:
        var dualBlades = (DualBlades)weapon;
        JsonSerializer.Serialize(writer, dualBlades, options);
        break;
      case WeaponType.Hammer:
        var hammer = (Hammer)weapon;
        JsonSerializer.Serialize(writer, hammer, options);
        break;
      case WeaponType.Lance:
        var lance = (Lance)weapon;
        JsonSerializer.Serialize(writer, lance, options);
        break;
      case WeaponType.Gunlance:
        var gunlance = (Gunlance)weapon;
        JsonSerializer.Serialize(writer, gunlance, options);
        break;
      case WeaponType.LightBowGun:
        var lightBowGun = (LightBowGun)weapon;
        JsonSerializer.Serialize(writer, lightBowGun, options);
        break;
      case WeaponType.HeavyBowGun:
        var heavyBowGun = (HeavyBowGun)weapon;
        JsonSerializer.Serialize(writer, heavyBowGun, options);
        break;
      case WeaponType.Bow:
        var bow = (Bow)weapon;
        JsonSerializer.Serialize(writer, bow, options);
        break;
      case WeaponType.SwitchAxe:
        var switchAxe = (SwitchAxe)weapon;
        JsonSerializer.Serialize(writer, switchAxe, options);
        break;
      case WeaponType.ChargeBlade:
        var chargeBlade = (ChargeBlade)weapon;
        JsonSerializer.Serialize(writer, chargeBlade, options);
        break;
      case WeaponType.HuntingHorn:
        var huntingHorn = (HuntingHorn)weapon;
        JsonSerializer.Serialize(writer, huntingHorn, options);
        break;
      case WeaponType.InsectGlaive:
        var insectGlaive = (InsectGlaive)weapon;
        JsonSerializer.Serialize(writer, insectGlaive, options);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    };
  }
}