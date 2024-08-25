using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.HeavyBowGuns;

namespace Serialization.Abstraction.Converters.Weapons;

public class HeavyBowGunConverter : JsonConverter<HeavyBowGun>, IJsonConverter
{
  public override HeavyBowGun? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();
    var ammo = root.ReadAmmo();

    var heavyBowGun = new HeavyBowGun(baseInfo.id,
      baseInfo.name,
      baseInfo.rarity,
      baseInfo.attack,
      baseInfo.affinity,
      baseInfo.defense,
      baseInfo.elementHidden,
      baseInfo.elementalStats,
      baseInfo.elderseal,
      baseInfo.decorationSlots,
      baseInfo.skills,
      ammo);
    return heavyBowGun;
  }

  public override void Write(Utf8JsonWriter writer, HeavyBowGun heavyBowGun, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(heavyBowGun);
    writer.WriteAmmo(heavyBowGun.BulletMechanic);
  }
}