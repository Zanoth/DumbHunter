using System.Text.Json;
using SharedDataModels.Abstractions.Gear.Ammos;
using SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Bullets;

namespace Serialization.Abstraction.Converters.Utils;

public static class AmmoWeaponConverterUtils
{
  public static BulletMechanic ReadAmmo(this JsonElement root)
  {
    var ammoConfigJson = root.GetProperty(nameof(BulletMechanic));

    var deviation = ammoConfigJson.GetProperty(nameof(BulletMechanic.Deviation)).GetInt32();

    var specialAmmoTypeStr = ammoConfigJson.GetProperty(nameof(BulletMechanic.Special)).GetString();
    var specialAmmoType = Enum.Parse<SpecialAmmoType>(specialAmmoTypeStr!);

    var ammoStats = new List<Ammo>();
    var ammoStatsArray = ammoConfigJson.GetProperty(nameof(BulletMechanic.SupportedAmmo)).EnumerateArray();
    while (ammoStatsArray.MoveNext())
    {
      var ammoStatJson = ammoStatsArray.Current;

      var typeStr = ammoStatJson.GetProperty(nameof(Ammo.AmmoType)).GetString();
      var type = Enum.Parse<AmmoType>(typeStr!);

      var clipSize = ammoStatJson.GetProperty(nameof(Ammo.ClipSize)).GetInt32();

      var rapidFire = ammoStatJson.GetProperty(nameof(Ammo.RapidFire)).GetBoolean();

      var recoil = ammoStatJson.GetProperty(nameof(Ammo.Recoil)).GetInt32();

      var reloadSpeedStr = ammoStatJson.GetProperty(nameof(Ammo.ReloadSpeed)).GetString();
      var reloadSpeed = Enum.Parse<RelodSpeed>(reloadSpeedStr!);

      var ammoStat = new Ammo(type, clipSize, rapidFire, recoil, reloadSpeed);
      ammoStats.Add(ammoStat);
    }

    var ammoMechanics = new BulletMechanic(deviation, specialAmmoType, ammoStats);
    return ammoMechanics;
  }
  public static void WriteAmmo(this Utf8JsonWriter writer, BulletMechanic bulletMechanic)
  {
    writer.WriteStartObject();

    writer.WriteNumber(nameof(BulletMechanic.Deviation), bulletMechanic.Deviation);
    writer.WriteString(nameof(BulletMechanic.Special), bulletMechanic.Special.ToString());

    writer.WriteStartArray(nameof(BulletMechanic.SupportedAmmo));
    foreach (var ammo in bulletMechanic.SupportedAmmo)
    {
      writer.WriteStartObject();

      writer.WriteString(nameof(Ammo.AmmoType), ammo.AmmoType.ToString());
      writer.WriteNumber(nameof(Ammo.ClipSize), ammo.ClipSize);
      writer.WriteBoolean(nameof(Ammo.RapidFire), ammo.RapidFire);
      writer.WriteNumber(nameof(Ammo.Recoil), ammo.Recoil);
      writer.WriteString(nameof(Ammo.ReloadSpeed), ammo.ReloadSpeed.ToString());

      writer.WriteEndObject();
    }
    writer.WriteEndArray();

    writer.WriteEndObject();
  }
}